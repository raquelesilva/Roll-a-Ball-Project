using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "Scriptable Objects/Material")]
public class MaterialSO: MonoBehaviour
{
    [SerializeField] bool isLocked = false;
    [SerializeField] Material material;
    [SerializeField] Sprite previewMaterial;

    // GETTERS
    public bool IsLocked() { return isLocked; }

    public Material GetMaterial() { return material; }

    public Sprite GetPreview() { return previewMaterial; }


    // SETTERS
    public void Unlock() { isLocked = false; }
}   