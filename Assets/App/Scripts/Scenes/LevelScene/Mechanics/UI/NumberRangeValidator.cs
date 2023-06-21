using TMPro;
using UnityEngine;

namespace UISpace
{
    public class NumberRangeValidator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private int _minValue;
        [SerializeField] private int _maxValue;

        private void OnEnable()
        {
            _inputField.onValueChanged.AddListener(ValidateInput);
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(ValidateInput);
        }
        
        private void ValidateInput(string text)
        {
            if (int.TryParse(text, out int value))
            {
                value = Mathf.Clamp(value, _minValue, _maxValue);
                _inputField.text = value.ToString();
            }
        }
    }
}