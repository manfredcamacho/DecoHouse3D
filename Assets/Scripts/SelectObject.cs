using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SelectObject : MonoBehaviour
{

    private float lastClickTime = 0;
    private const float DOUBLE_CLICK_TIME_THRESHOLD = 0.2f;
    private LayerMask interactableLayer;

    private void Awake()
    {
        interactableLayer = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;


            if (timeSinceLastClick <= DOUBLE_CLICK_TIME_THRESHOLD)
            {
                //Double Click
                DetectObject(Input.mousePosition);
            } 
            else 
            {
                //Simple Click
                lastClickTime = Time.time;
            }
        }
    }

    private void DetectObject(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, interactableLayer))
        {
            InteractableObject objet = hit.collider.GetComponent<InteractableObject>();

            if (objet != null)
            {
                objet.Interact();
            }

        }
    }
}
