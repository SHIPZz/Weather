using System;
using CodeBase.UI.AbstractWindow;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.InfoPopup
{
    public class InfoPopupWindow : AbstractWindowBase
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _okButton;
        
        public IObservable<Unit> OnOkButtonClicked => _okButton.OnClickAsObservable();
        
        public void Init(string title, string description)
        {
            _title.text = title;
            _description.text = description;
        }
    }
}