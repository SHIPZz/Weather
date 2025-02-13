using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.UI.AbstractWindow;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Facts
{
    public class FactWindow : AbstractWindowBase
    {
        [SerializeField] private RectTransform _factLayout;
        
        private IFactUIFactory _factUIFactory;
        private List<FactItemView> _factItems = new();
        private List<FactData> _factDatas = new();

        public event Action<string> FactSelected;
        public event Action Opened;

        [Inject]
        private void Construct(IFactUIFactory factUIFactory)
        {
            _factUIFactory = factUIFactory;
        }

        private void OnDisable() => UnsubscribeFactItemsEvents();

        public void Init(IReadOnlyList<FactData> datas)
        {
            _factDatas.AddRange(datas);
            
            CreateFactItems();

            SubscribeFactItemsEvents();
        }

        public override void Open()
        {
            Opened?.Invoke();
        }

        public void StopItemLoadingAnimation(string id)
        {
            _factItems.FirstOrDefault(x => x.ID == id)?.StopLoadingAnimation();
        }
        
        public void StopItemsLoadingAnimation()
        {
            _factItems.ForEach(x => x.StopLoadingAnimation());
        }

        public override void Close() => UnsubscribeFactItemsEvents();

        private void SubscribeFactItemsEvents() => _factItems.ForEach(x => x.Selected += SendFactItemSelectedEvent);

        private void UnsubscribeFactItemsEvents() => _factItems.ForEach(x => x.Selected -= SendFactItemSelectedEvent);

        private void SendFactItemSelectedEvent(string id) => FactSelected?.Invoke(id);

        private void CreateFactItems()
        {
            foreach (FactData factData in _factDatas)
            {
                _factItems.Add(_factUIFactory.CreateFactItem(_factLayout, factData));
            }
        }
    }
}