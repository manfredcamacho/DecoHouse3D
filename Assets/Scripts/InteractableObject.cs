using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string customName;

    public void Interact() {
        UIManager.Instance.ShowInteractionCanvas(this);
    }

    public string getCustomName()
    {
        return customName;
    }

}
