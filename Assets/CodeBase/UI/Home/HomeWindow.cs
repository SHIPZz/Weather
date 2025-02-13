using System;
using System.Collections.Generic;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Weather;
using UnityEngine;

namespace CodeBase.UI.Home
{
    public class HomeWindow : AbstractWindowBase
    {
        [SerializeField] private List<TabView> _tabs;
        [SerializeField] private WeatherView _weatherView;

        public event Action<TabTypeId> TabSelected;
        
        private void OnEnable()
        {
            _tabs.ForEach(x => x.Selected += SendTabSelectedEvent);
        }

        private void OnDisable()
        {
            _tabs.ForEach(x => x.Selected -= SendTabSelectedEvent);
        }

        private void SendTabSelectedEvent(TabTypeId tabTypeId)
        {
            TabSelected?.Invoke(tabTypeId);
        }

        public void UpdateWeatherUI(string degrees)
        {
            _weatherView.SetDegrees(degrees);
            Debug.Log($"{degrees}");
        }
    }
}