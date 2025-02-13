using UniRx;

namespace CodeBase.UI.Weather
{
    public class WeatherWindowController : IController<WeatherWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly IWeatherService _weatherService;

        public WeatherWindow View { get; private set; }
        
        public WeatherWindowController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        
        public void Initialize()
        {
            _weatherService
                .WeatherInfo
                .Subscribe(weather => View.UpdateWeather(weather))
                .AddTo(_compositeDisposable);
        }

        public void BindView(WeatherWindow value)
        {
            View = value;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}