using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private Canvas HUDCanvas;
    [SerializeField] private TMP_Text UITitle;
    [SerializeField] private Button texturesButton;

    public bool IsInspectPanelOpen { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
        UICanvas.gameObject.SetActive(false);
    }

    public void ShowInteractionCanvas(InteractableObject target)
    {
        IsInspectPanelOpen = true;
        UITitle.text = target.getCustomName();

        // Disable texture button if the object doesn't supports textures
        if (target.scriptableObject.isTextured && target.scriptableObject.texturedElements.Count > 0)
        {
            texturesButton.interactable = true;
        }
        else
        {
            texturesButton.interactable = false;
        }


        UICanvas.gameObject.SetActive(true);
    }
    public void CloseInspectPanel()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        player[0].gameObject.GetComponent<SmoothMover>().ReturnToOriginalPosition();
        IsInspectPanelOpen = false;
        UICanvas.gameObject.SetActive(false);
    }

    private void showMaterialColorPicker(InteractableObject target)
    {
        //if (target.editableMaterials != null && currentObject.editableMaterials.Count > 0)
        //{

        //    Color currentColor = currentObject.editableMaterials[0].color;

        //    rSlider.SetValueWithoutNotify(currentColor.r);
        //    gSlider.SetValueWithoutNotify(currentColor.g);
        //    bSlider.SetValueWithoutNotify(currentColor.b);

        //    //MOSTRAR LOS VALORES DEL RGB EN TEXTO EN PANTALLA (0-255)
        //    rValueText.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
        //    gValueText.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
        //    bValueText.text = Mathf.RoundToInt(currentColor.b * 255).ToString();

        //}
    }

    private void showTexturePickerPanel(InteractableObject target)
    {

    }
}
