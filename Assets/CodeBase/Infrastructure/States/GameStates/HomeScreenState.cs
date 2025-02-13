using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.Home;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.States.GameStates
{
    public class HomeScreenState : IState
    {
        private readonly IWindowService _windowService;

        public HomeScreenState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void Enter()
        {
            _windowService.OpenWindow<HomeWindow>();
        }

        public void Exit()
        {
            
        }
    }
}