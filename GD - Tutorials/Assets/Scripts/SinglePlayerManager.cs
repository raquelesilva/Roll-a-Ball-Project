using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerManager : MonoBehaviour
{
    [SerializeField] int points = 0;
    [SerializeField] float life = 100;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI pointsTxt;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private Image lifeBar;

    [SerializeField] public Transform currentWorld;
    [SerializeField] private Transform pickupObjsParent;
    [SerializeField] public List<GameObject> pickupObjs;
    [SerializeField] public List<GameObject> powerupsObjs;

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

    public void ResetGameUI()
    {
        points = 0;
        life = 100;
        pointsTxt.text = "Points: " + points.ToString();
        lifeBar.fillAmount = life  / 100;
    }

    public void SetWorld()
    {
        MenuManager.instance.ResumeGame();
        pauseWindow.SetActive(false);

        winWindow.SetActive(false);
        loseWindow.SetActive(false);

        if (currentWorld != null) 
        {
            Destroy(currentWorld.gameObject);
            currentWorld = null;
        }

        Level currentLevel = player.GetCurrentLevel();
        currentWorld = Instantiate(currentLevel.GetWorldPrefab()).transform;

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

        ResetGameUI();
    }

    public void CheckLife()
    {
        life -= 10;
        lifeBar.fillAmount = life / 100;

        if (life <= 0)
        {
            life = 0;

            MenuManager.instance.PauseGame();
            loseWindow.SetActive(true);
            nextLevelButton.SetActive(false);
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
            
            if (currentLevel >= LevelsManager.instance.levels.Count)
            {
                nextLevelButton.SetActive(false);
            }
            else
            {
                nextLevelButton.SetActive(true);
                LevelsManager.instance.levels[currentLevel].UnlockLevel();
            }

            MenuManager.instance.PauseGame();

            EnemySpawner.instance.DestroyAllEnemies();
        }
    }
}