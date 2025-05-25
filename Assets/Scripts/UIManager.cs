using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private Canvas HUDCanvas;
    [SerializeField] private TMP_Text title;

    public bool IsInspectPanelOpen { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
        UICanvas.gameObject.SetActive(false);
    }

    public void ShowInteractionCanvas(InteractableObject target)
    {
        UICanvas.gameObject.SetActive(true);
        IsInspectPanelOpen = true;
        title.text = target.getCustomName();
    }
    public void CloseInspectPanel()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        player[0].gameObject.GetComponent<SmoothMover>().ReturnToOriginalPosition();
        IsInspectPanelOpen = false;
        UICanvas.gameObject.SetActive(false);
    }
}
