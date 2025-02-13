using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Home
{
    public class TabView : MonoBehaviour
    {
        [field: SerializeField] public TabTypeId Id { get; private set; }

        [SerializeField] private Button _button;

        public event Action<TabTypeId> Selected; 

        private void OnEnable()
        {
            _button.onClick.AddListener(OnTabSelected);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnTabSelected);
        }

        private void OnTabSelected()
        {
            Selected?.Invoke(Id);
        }
    }
}