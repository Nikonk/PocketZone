using UnityEngine;
using UnityEngine.UI;

namespace PocketZone.Unit
{
    public class FloatingHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void UpdateHealthBar(int currentValue, int maxValue) => _slider.value = currentValue / (float)maxValue;
    }
}
