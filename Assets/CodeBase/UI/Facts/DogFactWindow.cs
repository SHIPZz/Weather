using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Models;
using CodeBase.UI.AbstractWindow;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Facts
{
    public class DogFactWindow : AbstractWindowBase
    {
        [SerializeField] private RectTransform _factLayout;
        
        private List<DogFactItemView> _factItems = new();
        private readonly List<DogFactData> _factDatas = new();

        private readonly Subject<string> _factSelected = new();

        private IDogFactUIFactory _dogFactUIFactory;
        
        public IObservable<string> FactSelected => _factSelected;

        [Inject]
        private void Construct(IDogFactUIFactory dogFactUIFactory) => _dogFactUIFactory = dogFactUIFactory;

        public void Init(IReadOnlyList<DogFactData> datas)
        {
            FillData(datas);
            CreateFactItems();
            SubscribeFactItemsEvents();
        }

        public void StopItemLoadingAnimation(string id) => _factItems.FirstOrDefault(x => x.ID == id)?.StopLoadingAnimation();

        public void ShowLoadingAnimation(string factItemId) => _factItems.FirstOrDefault(x => x.ID == factItemId)?.ShowLoadingAnimation();

        private void FillData(IReadOnlyList<DogFactData> datas) => _factDatas.AddRange(datas);

        private void SubscribeFactItemsEvents() => 
            _factItems.ForEach(x => x.Selected.Subscribe(_factSelected.OnNext)
                .AddTo(this));

        private void CreateFactItems()
        {
            foreach (DogFactData factData in _factDatas) 
                _factItems.Add(_dogFactUIFactory.CreateFactItem(_factLayout, factData));
        }
    }
}