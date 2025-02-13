using CodeBase.Gameplay.Common.Time;
using CodeBase.Gameplay.Dogs;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.Factory;
using CodeBase.Infrastructure.States.GameStates;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.ServersProcessing;
using CodeBase.StaticData;
using CodeBase.UI.Facts;
using CodeBase.UI.Services;
using CodeBase.UI.Services.Window;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {

        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindAssetManagementServices();
            BindCommonServices();
            BindGameplayServices();
            BindUIServices();
            BindGameStates();
            BindServerApiServices();
            BindUIFactories();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindUIFactories()
        {
            Container.Bind<IFactUIFactory>().To<FactUIFactory>().AsSingle();
        }

        private void BindServerApiServices()
        {
            Container.Bind<IServerApiService>().To<ServerApiService>().AsSingle();
            Container.Bind<IRequestQueueService>().To<RequestQueueService>().AsSingle();
        }
        
        private void BindUIServices()
        {
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<IUIProvider>().To<UIProvider>().AsSingle();
            Container.Bind<IUIStaticDataService>().To<UIStaticDataService>().AsSingle();
        }
        
        private void BindGameStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingHomeScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<HomeScreenState>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IDogService>().To<DogService>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindAssetManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}