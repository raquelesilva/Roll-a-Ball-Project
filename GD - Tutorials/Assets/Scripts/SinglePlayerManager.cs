using UnityEngine;

public class SinglePlayerManager : MonoBehaviour
{
    [SerializeField] Player playerSO;

    void Start()
    {
        Level currentLevel = playerSO.GetCurrentLevel();
        Instantiate(currentLevel.GetWorldPrefab());
    }
}