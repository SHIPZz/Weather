using System;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Home;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.States.States
{
    public class HomeScreenState : IState
    {
        private readonly IWindowService _windowService;
        private readonly IStateMachine _stateMachine;

        public HomeScreenState(IWindowService windowService, IStateMachine stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
        }

        public void Enter()
        {
            _stateMachine.Enter<WeatherTabState>();
            _windowService.OpenWindow<HomeWindow>();
        }

        public void Exit()
        {
            
        }
    }
}