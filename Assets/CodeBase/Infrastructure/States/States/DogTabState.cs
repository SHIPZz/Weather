using System.Threading;
using CodeBase.Gameplay.Dogs;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.Facts;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Weather;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States.States
{
    public class DogTabState : IState
    {
        private readonly IWindowService _windowService;
        private readonly IDogService _dogService;

        private CancellationTokenSource _cancellationToken = new();

        public DogTabState(IWindowService windowService, IDogService dogService)
        {
            _dogService = dogService;
            _windowService = windowService;
        }

        public void Enter()
        {
            _cancellationToken = new CancellationTokenSource();
            
            InitDogAsync(_cancellationToken.Token).Forget();
        }

        public void Exit()
        {
            if (!_cancellationToken.IsCancellationRequested)
                _cancellationToken?.Cancel();

            _cancellationToken?.Dispose();
            _windowService.Hide<DogFactWindow>();
        }

        private async UniTask InitDogAsync(CancellationToken cancellationToken)
        {
            await _dogService.GetDogFactsAsync(cancellationToken);
            
            _windowService.OpenWindow<DogFactWindow>();
        }
    }
}