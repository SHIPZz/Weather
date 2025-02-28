using System;
using System.Threading;
using CodeBase.ServersProcessing;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.Weather
{
    public class WeatherService : IWeatherService
    {
        private const float RequestPeriod = 5f;
                
        private readonly ReactiveProperty<string> _weatherInfo = new(string.Empty);
        private readonly CancellationTokenSource _cancellationToken = new();
        private readonly IServerApiService _serverApiService;
        private readonly IRequestQueueService _requestQueueService;
        
        public IReadOnlyReactiveProperty<string> WeatherInfo => _weatherInfo;

        public WeatherService(IServerApiService serverApiService, IRequestQueueService requestQueueService)
        {
            _requestQueueService = requestQueueService ?? throw new ArgumentNullException(nameof(requestQueueService));
            _serverApiService = serverApiService ?? throw new ArgumentNullException(nameof(serverApiService));
        }

        public async UniTask LaunchWeatherContinuouslyRequestingAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _requestQueueService.AddRequest(ProcessWeatherAsync);

                await UniTask.WaitForSeconds(RequestPeriod, true, PlayerLoopTiming.Update, cancellationToken,
                    cancelImmediately: true);
            }
        }

        public async UniTask ProcessWeatherAsync(CancellationToken cancellationToken)
        {
            try
            {
                string result = await _serverApiService.GetApiResponseAsync(ApiUrl.WeatherApiUrl, cancellationToken);
                WeatherResponse weatherResponse = JsonUtility.FromJson<WeatherResponse>(result);

                if (weatherResponse != null && weatherResponse.properties.periods.Count > 0)
                {
                    WeatherPeriod todayWeather = weatherResponse.properties.periods[0];

                    _weatherInfo.Value = $"Сегодня - {todayWeather.temperature}°F {todayWeather.shortForecast}";
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
        }

        public void Cleanup()
        {
            _requestQueueService.ClearQueue();
        }

        public void Dispose()
        {
            _weatherInfo?.Dispose();
            _cancellationToken?.Dispose();
        }
    }
}