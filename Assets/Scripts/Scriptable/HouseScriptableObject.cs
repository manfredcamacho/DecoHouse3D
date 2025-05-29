using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHouseSO", menuName = "Interactable/HouseScriptableObject")]

public class HouseScriptableObject : ScriptableObject
{
    public string customName;

    [Header("Material Settings")]
    public List<Material> customMaterialsToEdit;

    [Header("Texture Settings")]
    public bool isTextured = false;
    public Material texturedMaterial;
    public List<TexturedElement> texturedElements;

    [Header("Material Settings")]
    public Vector2 defaultTiling = Vector2.one;
}


[System.Serializable]
public class TexturedElement
{
    public Texture mainTexture;
    public Texture normalMap;
}