using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Palette : MonoBehaviour
{
    [SerializeField] UIManager UIManager;
    

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PickColor);

    }

    private void PickColor()
    {
        SoundManager.instance.PlaySound(SoundType.BUTTON_CLICK);
        Color color = transform.GetChild(0).GetComponent<Image>().color;
        UIManager.SetColorUI(color);
        UIManager.setColorTarget(color);
        
    }

}
