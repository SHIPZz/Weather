using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CodeBase.Gameplay.Dogs;
using CodeBase.ServersProcessing;
using CodeBase.UI.Facts;
using CodeBase.UI.Services.Window;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Home
{
    public class HomeWindowController : IController<HomeWindow>
    {
        public HomeWindow View { get; private set; }

        private readonly IRequestQueueService _requestQueueService;
        private readonly IServerApiService _serverApiService;
        private CancellationTokenSource _cancellationToken = new();
        private CancellationTokenSource _weatherContinuousRequesting = new();
        private readonly IDogService _dogService;
        private readonly IWindowService _windowService;

        public HomeWindowController(IRequestQueueService requestQueueService,
            IServerApiService serverApiService,
            IWindowService windowService,
            IDogService dogService)
        {
            _windowService = windowService;
            _dogService = dogService;
            _requestQueueService = requestQueueService;
            _serverApiService = serverApiService;
        }

        public void BindView(HomeWindow value)
        {
            View = value;
        }

        public void Initialize()
        {
            View.TabSelected += ProcessServerResponse;

            Debug.Log($"Hello");
            Debug.Log($"{View}");
        }

        public void Dispose()
        {
            View.TabSelected -= ProcessServerResponse;
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }

        private void ProcessServerResponse(TabTypeId selectedTabId)
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new();
            _requestQueueService.ClearQueue();
            
            switch (selectedTabId)
            {
                case TabTypeId.None:
                    break;

                case TabTypeId.Weather:
                    _requestQueueService.AddRequest(ProcessWeatherTab);
                    LaunchWeatherContinuouslyRequesting().Forget();
                    break;

                case TabTypeId.Dog:
                    _weatherContinuousRequesting?.Cancel();
                    _weatherContinuousRequesting?.Dispose();
                    _requestQueueService.AddRequest(ProcessDogFactsTab);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedTabId), selectedTabId, "Tab doesn't exist");
            }
        }

        private async UniTask LaunchWeatherContinuouslyRequesting()
        {
            _weatherContinuousRequesting = new();
            
            while (!_cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(5f, true, PlayerLoopTiming.Update, _weatherContinuousRequesting.Token, cancelImmediately: true);
                _requestQueueService.AddRequest(ProcessWeatherTab);
            }
        }

        private async UniTask ProcessWeatherTab()
        {
            try
            {
                string result = await _serverApiService.GetApiResponseAsync(ApiUrl.WeatherApiUrl, _cancellationToken.Token);
                WeatherResponse weatherResponse = JsonUtility.FromJson<WeatherResponse>(result);

                if (weatherResponse != null && weatherResponse.properties.periods.Count > 0)
                {
                    WeatherPeriod todayWeather = weatherResponse.properties.periods[0];
                    View.UpdateWeatherUI($"Сегодня - {todayWeather.temperature}°F {todayWeather.shortForecast}");
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Request weather was canceled");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Request weather error: {ex.Message}");
            }
            finally
            {
                _windowService.Hide<FactWindow>();
            }
        }

        private async UniTask ProcessDogFactsTab()
        {
            try
            {
                string result = await _serverApiService.GetApiResponseAsync(ApiUrl.DogApiUrl, _cancellationToken.Token);
                DogFactsResponse dogFactsResponse = JsonUtility.FromJson<DogFactsResponse>(result);

                if (dogFactsResponse != null && dogFactsResponse.data.Count > 0)
                {
                    IEnumerable<DogFact> targetDogs = dogFactsResponse.data.Take(10);

                    _dogService.Add(targetDogs);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Request dog fact was canceled");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Request dog fact error: {ex.Message}");
            }
            finally
            {
                _windowService.OpenWindow<FactWindow>();
            }
        }
    }
}