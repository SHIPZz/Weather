using CodeBase.UI.Services.Window;
using UniRx;
using Zenject;

namespace CodeBase.UI.InfoPopup
{
    public class InfoPopupController : IController<InfoPopupWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private IWindowService _windowService;
        
        public InfoPopupWindow View { get; private set; }

        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void BindView(InfoPopupWindow value)
        {
            View = value;
        }

        public void Initialize()
        {
            View.Exited.Subscribe(_ =>  ProcessExitEvent()).AddTo(_compositeDisposable);
        }

        private void ProcessExitEvent()
        {
            _windowService.Hide<InfoPopupWindow>();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}