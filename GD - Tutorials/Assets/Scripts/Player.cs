using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : MonoBehaviour
{
    [SerializeField] Material material;

    [SerializeField] int speedMultiplier;

    [SerializeField] Level currentLevel;

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    // GETTERS
    public Material GetMaterial () { return material; }

    public int GetSpeedMultiplier () { return speedMultiplier; }

    public Level GetCurrentLevel () { return currentLevel; }
    
    // SETTERS
    public void SetMaterial (Material chosenMaterial) 
    { 
        material = chosenMaterial;
        GetComponent<Renderer>().material = chosenMaterial;
    }

    public void SetSpeedMultiplier (int multiplier) { speedMultiplier = multiplier; }

    public void SetCurrentLevel (Level level) { currentLevel = level; }
}