using UnityEngine;

public class SinglePlayerManager : MonoBehaviour
{
    [SerializeField] Player player;

    void Start()
    {
        Level currentLevel = player.GetCurrentLevel();
        Instantiate(currentLevel.GetWorldPrefab());
    }
}