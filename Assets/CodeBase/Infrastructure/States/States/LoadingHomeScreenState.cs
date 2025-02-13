using System;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Facts;
using CodeBase.UI.Home;
using CodeBase.UI.InfoPopup;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Weather;

namespace CodeBase.Infrastructure.States.States
{
    public class LoadingHomeScreenState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _stateMachine;
        private readonly IWindowService _windowService;

        public LoadingHomeScreenState(ISceneLoader sceneLoader, IStateMachine stateMachine, IWindowService windowService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        }

        public void Enter()
        {
            BindWindows();

            _sceneLoader.LoadScene(Scenes.Home, () => _stateMachine.Enter<HomeScreenState>());
        }

        private void BindWindows()
        {
            _windowService.Bind<HomeWindow, HomeWindowController>();
            _windowService.Bind<DogFactWindow, DogTabWindowController>();
            _windowService.Bind<InfoPopupWindow, InfoPopupController>();
            _windowService.Bind<WeatherWindow, WeatherWindowController>();
        }

        public void Exit()
        {
            
        }
    }
}