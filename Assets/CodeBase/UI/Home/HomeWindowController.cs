using System;
using CodeBase.UI.Controllers;
using CodeBase.UI.Facts;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Weather;
using UniRx;

namespace CodeBase.UI.Home
{
    public class HomeWindowController : IController<HomeWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly IWindowService _windowService;
        
        private HomeWindow _view;

        public HomeWindowController(IWindowService windowService) => _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));

        public void BindView(HomeWindow value) => _view = value;

        public void Initialize() =>
            _view
                .TabSelected
                .Subscribe(OnTabSelected)
                .AddTo(_compositeDisposable);

        public void Dispose() => _compositeDisposable?.Dispose();

        private void OnTabSelected(TabTypeId selectedTabId)
        {
            switch (selectedTabId)
            {
                case TabTypeId.None:
                    break;

                case TabTypeId.Weather:
                    OpenWeatherWindow();
                    break;

                case TabTypeId.Dog:
                    OpenDogFactWindow();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedTabId), selectedTabId, "Tab doesn't exist");
            }
        }

        private void OpenDogFactWindow()
        {
            _windowService.Hide<WeatherWindow>();

            _windowService.OpenWindow<DogFactWindow>();
        }

        private void OpenWeatherWindow()
        {
            _windowService.Hide<DogFactWindow>();

            _windowService.OpenWindow<WeatherWindow>();
        }
    }
}