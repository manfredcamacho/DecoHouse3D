using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SelectObject : MonoBehaviour
{

    private float lastClickTime = 0;
    private const float DOUBLE_CLICK_TIME_THRESHOLD = 0.2f;
    private LayerMask interactableLayer;
    [SerializeField] private float reachDistance = 50f;
    private InteractableObject currentTarget;


    private void Awake()
    {
        interactableLayer = LayerMask.GetMask("Interactable");
    }

    void Update()
    {

        if (UIManager.Instance.IsInspectPanelOpen)
        {
            return;
        }


        // TODO FIX ON MOBILE, 
        //outlineInteractuableObject(Input.mousePosition);

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

        if (Physics.Raycast(ray, out hit, reachDistance, interactableLayer))
        {
            InteractableObject obj = hit.collider.GetComponent<InteractableObject>();

            if (obj != null)
            {
                //GetComponent<SmoothMover>().MoveToTarget(obj.gameObject.transform);
                obj.Interact();
            }

        }
    }

    private void outlineInteractuableObject(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance, interactableLayer))
        {
            InteractableObject target = hit.collider.GetComponent<InteractableObject>();

            if (target != currentTarget)
            {
                RemoveOutline();
                if (target.TryGetComponent<Outline>(out var outlineObj))
                {
                    currentTarget = target;
                    outlineObj.enabled = true;
                }
            }
        }
        else
        {
            RemoveOutline();
        }
    }

    private void RemoveOutline()
    {
        if (currentTarget && currentTarget.TryGetComponent<Outline>(out var outlineObj))
        {
            outlineObj.enabled = false;
        }
        currentTarget = null;
    }
}
