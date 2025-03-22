using System;
using System.Collections.Generic;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Tabs;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Home
{
    public class HomeWindow : AbstractWindowBase
    {
        [SerializeField] private Transform _tabLayout;

        private readonly Subject<TabTypeId> _tabSelected = new();
        private readonly List<TabView> _tabs = new();
        private ITabUIFactory _tabUIFactory;

        public IObservable<TabTypeId> TabSelected => _tabSelected;

        [Inject]
        private void Construct(ITabUIFactory tabUIFactory) => _tabUIFactory = tabUIFactory;

        public override void OnOpen()
        {
            _tabs.AddRange(_tabUIFactory.CreateAll(_tabLayout));
            _tabs.ForEach(x => x.Selected.Subscribe(SendTabSelectedEvent).AddTo(this));
        }

        private void SendTabSelectedEvent(TabTypeId tabTypeId) => _tabSelected?.OnNext(tabTypeId);
    }
}