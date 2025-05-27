
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _sliderInput;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _handleImage;
    [SerializeField] private Color _sliderColor = Color.white;

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = 255;

        SetColor(_sliderColor);

        _slider.onValueChanged.AddListener( (v) =>
        {
            _sliderInput.text = Mathf.Clamp(v, 0, 255).ToString("0");
        });

        _sliderInput.onEndEdit.AddListener((v) =>
        {
            if (float.TryParse(v, out float value))
            {
                value = Mathf.Clamp(value, 0, 255);
                _slider.value = value;
                _sliderInput.text = value.ToString("0");
            }
        });
    }

    public void SetColor(Color color)
    {
        if (_fillImage != null)
            _fillImage.color = color;

        if (_handleImage != null)
            _handleImage.color = color;
    }


}
