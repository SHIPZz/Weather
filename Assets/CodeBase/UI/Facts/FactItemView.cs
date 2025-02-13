using System;
using CodeBase.UI.Loading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Facts
{
    public class FactItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _button;
        
        [SerializeField] private LoadingView _loadingView;
       
        private string _id;

        public event Action<string> Selected;

        public string ID => _id;

        public void Init(string id, string rank, string name)
        {
            _id = id;
            _rank.text = rank;
            _name.text = name;

            _loadingView.Hide();
        }

        public void StopLoadingAnimation()
        {
            _loadingView.Hide();
        }

        private void OnEnable() => _button.onClick.AddListener(SendEvent);

        private void OnDisable() => _button.onClick.RemoveListener(SendEvent);

        private void SendEvent()
        {
            _loadingView.Show();
            Selected?.Invoke(_id);
        }
    }
}