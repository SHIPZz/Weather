using System;
using CodeBase.UI.Controllers;
using CodeBase.UI.Services.Window;
using UniRx;

namespace CodeBase.UI.InfoPopup
{
    public class InfoPopupController : IController<InfoPopupWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly IWindowService _windowService;
        
        private InfoPopupWindow _view;

        public InfoPopupController(IWindowService windowService) =>
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));

        public void BindView(InfoPopupWindow value) => _view = value;

        public void Initialize() => _view.OnOkButtonClicked
            .Subscribe(_ =>  _windowService.Hide<InfoPopupWindow>())
            .AddTo(_compositeDisposable);

        public void Dispose() => _compositeDisposable?.Dispose();
    }
}