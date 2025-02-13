using System;
using CodeBase.UI.Home;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Tabs
{
    public class TabView : MonoBehaviour
    {
        [field: SerializeField] public TabTypeId Id { get; private set; }

        [SerializeField] private Button _button;

        public IObservable<TabTypeId> Selected => _button.OnClickAsObservable().Select(_ => Id);
    }
}