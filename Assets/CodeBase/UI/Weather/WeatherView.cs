using CodeBase.UI.AbstractWindow;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Weather
{
    public class WeatherWindow : AbstractWindowBase
    {
        [SerializeField] private TMP_Text _weather;

        public void UpdateWeather(string weather)
        {
            _weather.text = weather;
        }
    }
}