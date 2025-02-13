using System;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.States.States;
using UniRx;

namespace CodeBase.UI.Home
{
    public class HomeWindowController : IController<HomeWindow>
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private readonly IStateMachine _stateMachine;

        public HomeWindow View { get; private set; }

        public HomeWindowController(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        public void BindView(HomeWindow value)
        {
            View = value;
        }

        public void Initialize()
        {
            View
                .TabSelected
                .Subscribe(OnTabSelected)
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private void OnTabSelected(TabTypeId selectedTabId)
        {
            switch (selectedTabId)
            {
                case TabTypeId.None:
                    break;

                case TabTypeId.Weather:
                    _stateMachine.Enter<WeatherTabState>();
                    break;

                case TabTypeId.Dog:
                    _stateMachine.Enter<DogTabState>();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedTabId), selectedTabId, "Tab doesn't exist");
            }
        }
        
    }
}