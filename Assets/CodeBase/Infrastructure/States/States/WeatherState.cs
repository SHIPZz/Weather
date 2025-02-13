using System.Threading;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Weather;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States.States
{
    public class WeatherTabState : IState
    {
        private readonly IWindowService _windowService;
        private readonly IWeatherService _weatherService;

        private CancellationTokenSource _cancellationToken = new();

        public WeatherTabState(IWindowService windowService, IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _windowService = windowService;
        }

        public void Enter()
        {
            _cancellationToken = new CancellationTokenSource();
            
            _weatherService.LaunchWeatherContinuouslyRequesting(_cancellationToken.Token).Forget();
         
            _windowService.OpenWindow<WeatherWindow>();
        }

        public void Exit()
        {
            if (!_cancellationToken.IsCancellationRequested)
                _cancellationToken?.Cancel();

            _cancellationToken?.Dispose();
            _weatherService.Cleanup();
            _windowService.Hide<WeatherWindow>();
        }
    }
}