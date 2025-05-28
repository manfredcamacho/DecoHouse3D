
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilingSliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _sliderInput;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private string _text;
    private void Start()
    {
        _slider.minValue = -20;
        _slider.maxValue = 20;

        _textMeshPro.text = _text;

        _slider.onValueChanged.AddListener( (v) =>
        {
            _sliderInput.text = Mathf.Clamp(v, -20, 20).ToString("0");
        });

        _sliderInput.onEndEdit.AddListener((v) =>
        {
            if (float.TryParse(v, out float value))
            {
                value = Mathf.Clamp(value, -20, 20);
                _slider.value = value;
                _sliderInput.text = value.ToString("0");
            }
        });
    }
}
