using System;
using System.Threading;
using CodeBase.UI.Controllers;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Weather
{
    public class WeatherWindowController : IController<WeatherWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly IWeatherService _weatherService;

        private WeatherWindow _view;
        private CancellationTokenSource _cancellationToken;

        public WeatherWindowController(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }
        
        public void Initialize()
        {
            _weatherService
                .WeatherInfo
                .Subscribe(weather => _view.UpdateWeather(weather))
                .AddTo(_compositeDisposable);
            
            _view
                .OnOpenEvent
                .Subscribe(_ => ProcessOpening())
                .AddTo(_compositeDisposable);
        }
        
        public void BindView(WeatherWindow value)
        {
            _view = value;
        }

        public void Dispose()
        {
            if (!_cancellationToken.IsCancellationRequested)
                _cancellationToken?.Cancel();

            _cancellationToken?.Dispose();
            
            _weatherService.Cleanup();
            
            _compositeDisposable?.Dispose();
        }

        private void ProcessOpening()
        {
            _cancellationToken = new CancellationTokenSource();
            
            _weatherService.LaunchWeatherContinuouslyRequestingAsync(_cancellationToken.Token).Forget();
        }
    }
}