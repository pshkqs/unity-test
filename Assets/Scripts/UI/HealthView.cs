using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthView : TextView
    {
        [SerializeField] private Image _healthImage;
        
        public void OnChanged(int value)
        {
            base.OnChanged(value);
            _healthImage.fillAmount = value / 100f;
        }
    }
}