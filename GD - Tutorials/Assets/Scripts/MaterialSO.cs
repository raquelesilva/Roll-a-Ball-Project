using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "Scriptable Objects/Material")]
public class MaterialSO: ScriptableObject
{
    [SerializeField] bool isUnlocked = false;
    [SerializeField] Material material;

    // GETTERS
    public bool GetState() { return isUnlocked; }

    public Material GetMaterial() { return material; }

    // SETTERS
    public void SetUnlocked() { isUnlocked = true; }
}   