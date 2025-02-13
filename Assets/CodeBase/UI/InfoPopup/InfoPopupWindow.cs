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

        private readonly Subject<Unit> _exited = new();
        private readonly Subject<Unit> _opened = new();

        public IObservable<Unit> Exited => _exited;
        public IObservable<Unit> Opened => _opened;

        public void Init(string title, string description)
        {
            _title.text = title;
            _description.text = description;
        }

        public override void Open() => _opened.OnNext(Unit.Default);

        private void Start() => _okButton.onClick.AsObservable().Subscribe(_ => SendExitEvent()).AddTo(this);

        private void SendExitEvent() => _exited.OnNext(Unit.Default);
    }
}