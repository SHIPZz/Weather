using System;
using CodeBase.UI.Loading;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Facts
{
    public class DogFactItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _button;
        
        [SerializeField] private LoadingView _loadingView;
       
        private string _id;

        public IObservable<string> Selected => _button.OnClickAsObservable().Select(_ => _id);

        public string ID => _id;

        public void Init(string id, string rank, string name)
        {
            _id = id;
            _rank.text = rank;
            _name.text = name;

            _loadingView.Hide();
        }

        public void StopLoadingAnimation() => _loadingView.Hide();

        public void ShowLoadingAnimation() => _loadingView.Show();
    }
}