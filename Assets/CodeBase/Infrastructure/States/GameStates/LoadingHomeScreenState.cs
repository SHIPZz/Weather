using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Facts;
using CodeBase.UI.Home;
using CodeBase.UI.InfoPopup;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.States.GameStates
{
    public class LoadingHomeScreenState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;

        public LoadingHomeScreenState(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IWindowService windowService)
        {
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            BindWindows();

            _sceneLoader.LoadScene(Scenes.Home, () => _gameStateMachine.Enter<HomeScreenState>());
        }

        private void BindWindows()
        {
            _windowService.Bind<HomeWindow, HomeWindowController>();
            _windowService.Bind<FactWindow, FactWindowController>();
            _windowService.Bind<InfoPopupWindow, InfoPopupController>();
        }

        public void Exit()
        {
            
        }
    }
}