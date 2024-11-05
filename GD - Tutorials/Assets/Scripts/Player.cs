using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : MonoBehaviour
{
    [SerializeField] Material material;

    [SerializeField] int speedMultiplier;

    [SerializeField] Level currentLevel;

    LevelsManager levelsManager;

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelsManager = LevelsManager.instance;
    }

    // GETTERS
    public Material GetMaterial () { return material; }

    public int GetSpeedMultiplier () { return speedMultiplier; }

    public Level GetCurrentLevel () { return currentLevel; }

    public int GetCurrentLevelINT () 
    {
        int currentLevelInt = 0;

        Debug.Log(levelsManager);
        Debug.Log(levelsManager.levels);
        Debug.Log(levelsManager.levels.Count);

        for (int i = 0; i < levelsManager.levels.Count; i++)
        {
            Level level = levelsManager.levels[i];
            if (level == currentLevel)
            {
                currentLevelInt = i;
            }
        }

        return currentLevelInt; 
    }
    
    // SETTERS
    public void SetMaterial (Material chosenMaterial) 
    { 
        material = chosenMaterial;
        GetComponent<Renderer>().material = chosenMaterial;
    }

    public void SetSpeedMultiplier (int multiplier) { speedMultiplier = multiplier; }

    public void SetCurrentLevel (Level level) { currentLevel = level; }

    public void SetCurrentLevelINT (int level) { currentLevel = levelsManager.levels[level]; }
}