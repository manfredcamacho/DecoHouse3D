using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Scriptable Object")]
    public HouseScriptableObject scriptableObject;

    [Header("Runtime")]
    [HideInInspector] public Material[] currentMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();
    private int currentTextureIndex = 0;

    void Start()
    {
        SetupMaterials();
    }

    public void Interact() {
        UIManager.Instance.ShowInteractionCanvas(this);
    }

    public string getCustomName()
    {
        return scriptableObject.customName;
    }

    void SetupMaterials()
    {
        Renderer rend = GetComponent<Renderer>();
        if (!rend || scriptableObject == null)
        {
            return;
        }

        // Clone materials to avoid shared asset modification
        Material[] originalMats = rend.sharedMaterials;
        currentMaterials = new Material[originalMats.Length];

        for (int i = 0; i < originalMats.Length; i++)
        {
            currentMaterials[i] = originalMats[i] ? new Material(originalMats[i]) : null;
        }

        rend.materials = currentMaterials;

        // Select editable materials
        editableMaterials.Clear();

        if (scriptableObject.customMaterialsToEdit != null && scriptableObject.customMaterialsToEdit.Count > 0)
        {
            foreach (var matToEdit in scriptableObject.customMaterialsToEdit)
            {
                foreach (var mat in currentMaterials)
                {
                    if (mat != null && mat.name.StartsWith(matToEdit.name))
                    {
                        editableMaterials.Add(mat);
                    }
                }
            }
        }
        else
        {
            editableMaterials.AddRange(currentMaterials); // Edit all if none specified
        }


        if (scriptableObject.isTextured && scriptableObject.texturedMaterial && scriptableObject.texturedElements.Count > 0)
        {
            ApplyTexture(scriptableObject.texturedElements[0]);
        }
    }
    private void ApplyTexture(TexturedElement element)
    {
        if (!scriptableObject.texturedMaterial || element == null) return;

        foreach (var mat in currentMaterials)
        {
            if (mat != null && mat.name.StartsWith(scriptableObject.texturedMaterial.name))
            {
                mat.mainTexture = element.mainTexture;

                if (element.normalMap != null)
                {
                    mat.EnableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", element.normalMap);
                }
                break;
            }
        }
    }

    public void SetColor(Color newColor)
    {
        foreach (var mat in editableMaterials)
        {
            if (mat != null)
            {
                mat.color = newColor;
            }
        }
    }

    public void SetTexture(int index)
    {
        if (!IsTextureValid(index)) return;
        currentTextureIndex = index;
        ApplyTexture(scriptableObject.texturedElements[currentTextureIndex]);
    }

    public void NextTexture()
    {
        if (!IsTextureValid()) return;

        currentTextureIndex++;
        if (currentTextureIndex >= scriptableObject.texturedElements.Count)
            currentTextureIndex = 0;

        ApplyTexture(scriptableObject.texturedElements[currentTextureIndex]);

    }

    public void PreviousTexture()
    {
        if (!IsTextureValid()) return;

        currentTextureIndex--;
        if (currentTextureIndex < 0)
            currentTextureIndex = scriptableObject.texturedElements.Count - 1;

        ApplyTexture(scriptableObject.texturedElements[currentTextureIndex]);

    }

    private bool IsTextureValid(int index = -1)
    {
        return scriptableObject.isTextured &&
               scriptableObject.texturedMaterial != null &&
               scriptableObject.texturedElements != null &&
               scriptableObject.texturedElements.Count > 0 &&
               (index == -1 || (index >= 0 && index < scriptableObject.texturedElements.Count));
    }

    public void SetTiling(Vector2 newTiling)
    {
        foreach (Material mat in editableMaterials)
        {
            if (mat != null)
                mat.mainTextureScale = newTiling;
        }
    }


    public void SetTextureByIndex(int index)
    {
        if (scriptableObject == null || scriptableObject.texturedElements == null || index < 0 || index >= scriptableObject.texturedElements.Count)
            return;

        TexturedElement selectedElement = scriptableObject.texturedElements[index];

        foreach (Material mat in editableMaterials)
        {
            if (mat != null)
            {
                mat.mainTexture = selectedElement.mainTexture;

                if (selectedElement.normalMap != null)
                {
                    mat.EnableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", selectedElement.normalMap);
                }
                else
                {
                    mat.DisableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", null); // Clear previous bump
                }
            }
        }

        currentTextureIndex = index;
    }

}
