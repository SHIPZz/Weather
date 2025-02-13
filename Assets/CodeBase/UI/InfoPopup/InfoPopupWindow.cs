using System;
using CodeBase.UI.AbstractWindow;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.InfoPopup
{
    public class InfoPopupWindow : AbstractWindowBase
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _okButton;

        public event Action Exited;
        public event Action Opened;
        
        public void Init(string title, string description)
        {
            _title.text = title;
            _description.text = description;
        }

        public override void Open()
        {
            Opened?.Invoke();
        }

        private void OnEnable() => _okButton.onClick.AddListener(SendExitEvent);

        private void OnDisable() => _okButton.onClick.RemoveListener(SendExitEvent);

        private void SendExitEvent() => Exited?.Invoke();
    }
}