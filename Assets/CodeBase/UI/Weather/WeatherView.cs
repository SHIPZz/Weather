using TMPro;
using UnityEngine;

namespace CodeBase.UI.Weather
{
    public class WeatherView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _degrees;

        public void SetDegrees(string degrees)
        {
            _degrees.text = degrees;
        }
    }
}