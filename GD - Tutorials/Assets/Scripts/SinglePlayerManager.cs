using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerManager : MonoBehaviour
{
    [SerializeField] int points = 0;
    [SerializeField] float life = 100;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI pointsTxt;
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject loseWindow;
    [SerializeField] Image lifeBar;

    [SerializeField] Transform pickupObjsParent;
    [SerializeField] public List<GameObject> pickupObjs;

    Player player;
    PlayerController playerController;

    public static SinglePlayerManager instance;

    private void Awake()
    {
        instance = this;

        playerController = PlayerController.instance;

        //playerController.singlePlayerManager = this;
    }

    void Start()
    {
        player = Player.instance;
    }

    public void SetWorld()
    {
        Level currentLevel = player.GetCurrentLevel();
        Transform currentWorld = Instantiate(currentLevel.GetWorldPrefab()).transform;

        player.transform.position = Vector3.up + currentWorld.position + Vector3.up;
        player.GetComponent<Rigidbody>().useGravity = true;

        pickupObjsParent = currentWorld.GetChild(currentWorld.childCount - 1);

        if (pickupObjsParent != null)
        {
            pickupObjs.Clear();
            for (int i = 0; i < pickupObjsParent.childCount; i++)
            {
                pickupObjs.Add(pickupObjsParent.GetChild(i).gameObject);
            }
        }

        pointsTxt.text = "Points: " + points.ToString();
        lifeBar.fillAmount = life / 100;
    }

    public void CheckLife()
    {
        life -= 10;
        lifeBar.fillAmount = life / 100;

        if (life <= 0)
        {
            life = 0;
            loseWindow.SetActive(true);

            //Destroy(player.gameObject);
        }
    }

    public void CheckPoints()
    {
        points += 5;

        pointsTxt.text = "Points: " + points.ToString();

        if (points >= pickupObjs.Count * 5)
        {
            winWindow.SetActive(true);

            Level currentLevelGO = player.GetCurrentLevel();
            int currentLevel = currentLevelGO.GetLevel();
            //LevelsManager.instance.levels[1];

            MenuManager.instance.PauseGame();

            EnemySpawner.instance.DestroyAllEnemies();

            LevelsManager.instance.levels[currentLevel + 1].UnlockLevel();
        }
    }
}