using System;
using CodeBase.Gameplay.Dogs;
using CodeBase.UI.Services.Window;
using Zenject;

namespace CodeBase.UI.InfoPopup
{
    public class InfoPopupController : IController<InfoPopupWindow>, IDisposable
    {
        private IWindowService _windowService;
        private IDogService _dogService;
        public InfoPopupWindow View { get; private set; }

        [Inject]
        private void Construct(IWindowService windowService, IDogService dogService)
        {
            _dogService = dogService;
            _windowService = windowService;
        }

        public void Initialize()
        {
            View.Exited += ProcessExitEvent;
            View.Opened += InitView;
        }

        private void InitView()
        {
            DogFact lastSelectedDog = _dogService.GetLastSelectedDog();

            if (lastSelectedDog != null)
                View.Init(lastSelectedDog.attributes.name, lastSelectedDog.attributes.description);
        }

        public void BindView(InfoPopupWindow value)
        {
            View = value;
        }

        public void Dispose()
        {
            View.Exited -= ProcessExitEvent;
        }

        private void ProcessExitEvent()
        {
            _windowService.Hide<InfoPopupWindow>();
        }
    }
}