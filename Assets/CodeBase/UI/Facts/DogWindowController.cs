using System;
using System.Threading;
using CodeBase.Extensions;
using CodeBase.Gameplay.Dogs;
using CodeBase.UI.InfoPopup;
using CodeBase.UI.Services.Window;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Facts
{
    public class DogTabWindowController : IController<DogFactWindow>
    {
        private readonly IDogService _dogService;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly IWindowService _windowService;

        private CancellationTokenSource _cancellationToken = new();
        private string _lastFactId;

        public DogFactWindow View { get; private set; }

        public DogTabWindowController(IDogService dogService, IWindowService windowService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _dogService = dogService ?? throw new ArgumentNullException(nameof(dogService));
        }

        public void Initialize()
        {
            View
                .Opened
                .Subscribe(_ => InitView())
                .AddTo(_compositeDisposable);

            View
                .FactSelected
                .Subscribe(id => ProcessGettingDogFactAsync(id).Forget())
                .AddTo(_compositeDisposable);
        }

        private async UniTask ProcessGettingDogFactAsync(string id)
        {
            DogFact dogFact;
            
            try
            {
                Cleanup();
                _cancellationToken = new();
                _windowService.Hide<InfoPopupWindow>();

                View.ShowLoadingAnimation(id);

                dogFact = await _dogService.GetDogFactAsync(id, _cancellationToken.Token);

                _windowService
                    .OpenWindow<InfoPopupWindow>()
                    .Init(dogFact.attributes.name, dogFact.attributes.description);
                
                View.StopItemLoadingAnimation(id);
            }
            catch (Exception e)
            {
                _windowService.Hide<InfoPopupWindow>();
            }
        }

        public void BindView(DogFactWindow value)
        {
            View = value;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();

            Cleanup();
        }

        private void InitView()
        {
            View.Init(_dogService.GetAll().AsFactDataList());
        }

        private void Cleanup()
        {
            if (!_cancellationToken.IsCancellationRequested)
                _cancellationToken?.Cancel();

            _cancellationToken?.Dispose();
        }
    }
}