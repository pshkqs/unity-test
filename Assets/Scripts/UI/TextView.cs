using TMPro;
using UnityEngine;

namespace UI
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _text;
        [SerializeField] protected string _placeholder;

        protected void OnChanged(params object[] values)
        {
            _text.text = string.Format(_placeholder, values);
        }
    }
}