using CodeBase.Gameplay.Dogs;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.Factory;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.States.States;
using CodeBase.ServersProcessing;
using CodeBase.StaticData;
using CodeBase.UI.Facts;
using CodeBase.UI.Services;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Tabs;
using CodeBase.UI.Weather;
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
            BindAppServices();
            BindUIServices();
            BindStates();
            BindServerApiServices();
            BindUIFactories();

            Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();
        }

        private void BindUIFactories()
        {
            Container.Bind<IDogFactUIFactory>().To<DogFactUIFactory>().AsSingle();
            Container.Bind<ITabUIFactory>().To<TabUIFactory>().AsSingle();
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
        
        private void BindStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingHomeScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<HomeScreenState>().AsSingle();
        }

        private void BindAppServices()
        {
            Container.Bind<IDogService>().To<DogService>().AsSingle();
            Container.Bind<IWeatherService>().To<WeatherService>().AsSingle();
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
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IStateMachine>().Enter<BootstrapState>();
        }
    }
}