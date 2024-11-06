using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class WorldHolder : MonoBehaviour
{
    [SerializeField] Material materialPlayer1;
    [SerializeField] Material materialPlayer2;

    [SerializeField] int speedMultiplier;

    [SerializeField] Level currentLevel;

    LevelsManager levelsManager;

    public static WorldHolder instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelsManager = LevelsManager.instance;
    }

    // GETTERS
    public Material GetMaterial () { return materialPlayer1; }

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
        materialPlayer1 = chosenMaterial;
        GameManager.instance.GetPlayer1().GetComponent<Renderer>().material = chosenMaterial;
    }

    public void SetSpeedMultiplier (int multiplier) { speedMultiplier = multiplier; }

    public void SetCurrentLevel (Level level) { currentLevel = level; }

    public void SetCurrentLevelINT (int level) { currentLevel = levelsManager.levels[level]; }
}