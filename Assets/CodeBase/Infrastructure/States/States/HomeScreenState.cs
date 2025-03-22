using System;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.Home;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Weather;

namespace CodeBase.Infrastructure.States.States
{
    public class HomeScreenState : IState
    {
        private readonly IWindowService _windowService;

        public HomeScreenState(IWindowService windowService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
        }

        public void Enter()
        {
            _windowService.OpenWindow<WeatherWindow>();
            _windowService.OpenWindow<HomeWindow>();
        }

        public void Exit()
        {
            
        }
    }
}