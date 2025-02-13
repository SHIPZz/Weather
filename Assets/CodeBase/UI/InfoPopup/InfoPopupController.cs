using CodeBase.UI.Services.Window;
using UniRx;
using Zenject;

namespace CodeBase.UI.InfoPopup
{
    public class InfoPopupController : IController<InfoPopupWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private IWindowService _windowService;
        private InfoPopupWindow _view;

        [Inject]
        private void Construct(IWindowService windowService) => _windowService = windowService;

        public void BindView(InfoPopupWindow value) => _view = value;

        public void Initialize() => _view.Exited.Subscribe(_ =>  ProcessExitEvent()).AddTo(_compositeDisposable);

        private void ProcessExitEvent() => _windowService.Hide<InfoPopupWindow>();

        public void Dispose() => _compositeDisposable?.Dispose();
    }
}