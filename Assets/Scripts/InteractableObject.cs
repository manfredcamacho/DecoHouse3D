using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public HouseScriptableObject scriptableObject;

    public void Interact() {
        UIManager.Instance.ShowInteractionCanvas(this);
    }

    public string getCustomName()
    {
        return scriptableObject.customName;
    }

}
