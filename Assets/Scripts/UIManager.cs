using TMPro;
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
    [SerializeField] private GameObject texturesContainer;
    [SerializeField] private GameObject paletteContainer;


    [Header("Sliders Colors")]
    [SerializeField] private Slider rSlider;
    [SerializeField] private Slider gSlider;
    [SerializeField] private Slider bSlider;

    [Header("Sliders Colors Input")]

    [SerializeField] private TMP_InputField rInput;
    [SerializeField] private TMP_InputField gInput;
    [SerializeField] private TMP_InputField bInput;

    [Header("Tiling Sliders")]
    [SerializeField] private Slider tilingXSlider;
    [SerializeField] private Slider tilingYSlider;

    [Header("Tiling Input")]
    [SerializeField] private TMP_InputField tilingXValueText;
    [SerializeField] private TMP_InputField tilingYValueText;


    [Header("Texture Selection Grid")]
    [SerializeField] private GameObject textureButtonPrefab; // A prefab with Image + Button
    [SerializeField] private Transform textureGridParent;

    private InteractableObject currentObject;

    public bool IsInspectPanelOpen { get; private set; } = false;

    private void Awake()
    {
        Instance = this;

        rSlider.onValueChanged.AddListener(OnSliderValueChanged);
        gSlider.onValueChanged.AddListener(OnSliderValueChanged);
        bSlider.onValueChanged.AddListener(OnSliderValueChanged);

        tilingXSlider.onValueChanged.AddListener(OnTilingSliderChanged);
        tilingYSlider.onValueChanged.AddListener(OnTilingSliderChanged);
        
        UICanvas.gameObject.SetActive(false);
    }

    public void ShowInteractionCanvas(InteractableObject target)
    {
        SoundManager.instance.PlaySound(SoundType.BUTTON_CLICK);
        currentObject = target;

        HUDCanvas.enabled = false;

        var outline = currentObject.GetComponent<Outline>();
        if (outline != null) { outline.enabled = false; }


        IsInspectPanelOpen = true;
        UITitle.text = currentObject.getCustomName();

        // Disable texture button if the object doesn't supports textures
        if (currentObject.scriptableObject.isTextured && currentObject.scriptableObject.texturedElements.Count > 0)
        {
            texturesButton.interactable = true;
        }
        else
        {
            texturesButton.interactable = false;
        }

        // Set tiling values if materials exist
        if (currentObject.editableMaterials != null && currentObject.editableMaterials.Count > 0)
        {
            Vector2 currentTiling = currentObject.editableMaterials[0].mainTextureScale;

            tilingXSlider.SetValueWithoutNotify(currentTiling.x);
            tilingYSlider.SetValueWithoutNotify(currentTiling.y);

            tilingXValueText.text = currentTiling.x.ToString("0.00");
            tilingYValueText.text = currentTiling.y.ToString("0.00");
        }

        PopulateTextureGrid();


        // Change Material Color
        if (currentObject.editableMaterials != null && currentObject.editableMaterials.Count > 0)
        {
            Color currentColor = currentObject.editableMaterials[0].color;

            SetColorUI(currentColor);
        }


        UICanvas.gameObject.SetActive(true);
    }
    public void CloseInspectPanel()
    {
        //GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        //player[0].gameObject.GetComponent<SmoothMover>().ReturnToOriginalPosition();

        HUDCanvas.enabled = true;

        SoundManager.instance.PlaySound(SoundType.BUTTON_CLOSE);
        IsInspectPanelOpen = false;
        paletteContainer.SetActive(true);
        texturesContainer.SetActive(false);
        UICanvas.gameObject.SetActive(false);
    }

    public void OnTilingSliderChanged(float value)
    {
        if (currentObject == null) return;

        Vector2 newTiling = new Vector2(tilingXSlider.value, tilingYSlider.value);
        currentObject.SetTiling(newTiling);

        tilingXValueText.text = newTiling.x.ToString("0.00");
        tilingYValueText.text = newTiling.y.ToString("0.00");
    }

    private void PopulateTextureGrid()
    {
        foreach (Transform child in textureGridParent)
        {
            Destroy(child.gameObject);
        }

        if (currentObject == null || currentObject.scriptableObject == null || currentObject.scriptableObject.texturedElements == null)
            return;

        for (int i = 0; i < currentObject.scriptableObject.texturedElements.Count; i++)
        {
            int index = i; // Needed to avoid closure issue in lambda

            GameObject buttonGO = Instantiate(textureButtonPrefab, textureGridParent);
            RawImage image = buttonGO.GetComponentInChildren<RawImage>();
            image.texture = currentObject.scriptableObject.texturedElements[i].mainTexture;

            buttonGO.GetComponent<Button>().onClick.AddListener(() =>
            {
                currentObject.SetTextureByIndex(index);
                SoundManager.instance.PlaySound(SoundType.BUTTON_CLICK, 2.0f);
            });
        }
    }

    public void OnSliderValueChanged(float value)
    {
        SoundManager.instance.PlaySound(SoundType.BUTTON_HOVER);

        if (currentObject == null || currentObject.currentMaterials == null) return;

        Color newColor = new Color(rSlider.value, gSlider.value, bSlider.value);

        setColorTarget(newColor); // Let the object apply the color

        // Update RGB text values (0-255)
        rInput.text = Mathf.RoundToInt(newColor.r * 255).ToString();
        gInput.text = Mathf.RoundToInt(newColor.g * 255).ToString();
        bInput.text = Mathf.RoundToInt(newColor.b * 255).ToString();
    }

    public void SetColorUI(Color newColor)
    {
        rSlider.SetValueWithoutNotify(newColor.r);
        gSlider.SetValueWithoutNotify(newColor.g);
        bSlider.SetValueWithoutNotify(newColor.b);

        rInput.text = Mathf.RoundToInt(newColor.r * 255).ToString();
        gInput.text = Mathf.RoundToInt(newColor.g * 255).ToString();
        bInput.text = Mathf.RoundToInt(newColor.b * 255).ToString();
    }

    public void setColorTarget(Color newColor)
    {
        if (currentObject != null)
        {
            currentObject.SetColor(newColor);
        }
    }
}
