using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : ScriptableObject
{
    [SerializeField] MaterialSO material;

    [SerializeField] int speedMultiplier;

    [SerializeField] Level currentLevel;
    
    // GETTERS
    public MaterialSO GetMaterial () { return material; }

    public int GetSpeedMultiplier () { return speedMultiplier; }

    public Level GetCurrentLevel () { return currentLevel; }
    
    // SETTERS
    public void SetMaterial (MaterialSO chosenMaterial) { material = chosenMaterial; }

    public void SetSpeedMultiplier (int multiplier) { speedMultiplier = multiplier; }

    public void SetCurrentLevel (Level level) { currentLevel = level; }
}