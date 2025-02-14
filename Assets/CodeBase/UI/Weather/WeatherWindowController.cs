using System;
using CodeBase.UI.Controllers;
using UniRx;

namespace CodeBase.UI.Weather
{
    public class WeatherWindowController : IController<WeatherWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly IWeatherService _weatherService;

        private WeatherWindow _view;
        
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
        }

        public void BindView(WeatherWindow value)
        {
            _view = value;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}