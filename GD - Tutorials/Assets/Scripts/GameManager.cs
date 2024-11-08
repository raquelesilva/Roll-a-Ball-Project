using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameType gameType;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;

    [SerializeField] int points = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI pointsTxt;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject winStoryWindow;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI powerupsTxt;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private Image lifeBar1;
    [SerializeField] private Image lifeBar2;
    [SerializeField] List<GameObject> stars = new();

    [SerializeField] public Transform currentWorld;
    [SerializeField] private Transform pickupObjsParent;
    [SerializeField] public List<GameObject> pickupObjs;
    [SerializeField] public List<GameObject> powerupsObjs;

    [SerializeField] public float timer;
    [SerializeField] public int score;
    [SerializeField] public int powerups;
    [SerializeField] private bool playTimer;

    WorldHolder worldHolder;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        worldHolder = WorldHolder.instance;
    }

    private void Update()
    {
        if (playTimer)
        {
            timer += Time.deltaTime;
        }
    }

    public void ResetGameUI()
    {
        points = 0;
        powerups = 0;
        player1.GetHealth().SetupLifes();
        player2.GetHealth().SetupLifes();
        pointsTxt.text = "Points: " + points.ToString();

        MenuManager.instance.ResumeGame();
        pauseWindow.SetActive(false);

        winWindow.SetActive(false);
        loseWindow.SetActive(false);
        winStoryWindow.SetActive(false);
    }

    public void SetGameType(int newGameType)
    {
        gameType = (GameType)newGameType;
    }

    public void SetWorld()
    {
        player1.gameObject.SetActive(true);
        player1.GetRigidBody().useGravity = true;
        player1.GetHealth().SetupLifes();

        player1.movePlayer = true;

        player2.gameObject.SetActive(gameType == GameType.MultiPlayer);
        player2.GetRigidBody().useGravity = gameType == GameType.MultiPlayer;

        if(gameType == GameType.MultiPlayer)
        {
            player2.GetHealth().SetupLifes();
            lifeBar2.gameObject.SetActive(true);
            player2.movePlayer = true;
            CameraController.instance.SetFollow(false);
        }
        else
        {
            lifeBar2.gameObject.SetActive(false);

            CameraController.instance.SetFollow(true);
        }

        playTimer = true;

        if (currentWorld != null)
        {
            Destroy(currentWorld.gameObject);
            currentWorld = null;
        }

        Level currentLevel = worldHolder.GetCurrentLevel();
        currentWorld = Instantiate(currentLevel.GetWorldPrefab()).transform;

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

    public void LoseGame()
    {
        MenuManager.instance.PauseGame();
        loseWindow.SetActive(true);
        nextLevelButton.SetActive(false);
        playTimer = false;
    }

    public void CheckPoints()
    {
        points += 5;

        pointsTxt.text = "Points: " + points.ToString();

        if (points >= pickupObjs.Count * 5)
        {
            MenuManager.instance.PauseGame();

            if (timer <= 1)
            {
                for (int i = 0; i < stars.Count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
            else if (timer > 1 && timer < 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    stars[i].SetActive(true);
                }

                stars[2].SetActive(false);
            }
            else
            {
                stars[0].SetActive(true);
              
                for (int i = 1; i < stars.Count; i++)
                {
                    stars[i].SetActive(false);
                }
            }

            playTimer = false;
            winWindow.SetActive(true);

            timerTxt.text = timer.ToString();
            scoreTxt.text = score.ToString();
            powerupsTxt.text = powerups.ToString();

            Level currentLevelGO = worldHolder.GetCurrentLevel();
            int currentLevel = currentLevelGO.GetLevel();

            if (currentLevel >= LevelsManager.instance.levels.Count)
            {
                nextLevelButton.SetActive(false);

                if (gameType == GameType.StoryMode)
                {
                    winStoryWindow.SetActive(true);

                    List<Level> levels = LevelsManager.instance.levels;

                    for (int i = 1; i < levels.Count; i++)
                    {
                        levels[i].StoryLevels(true);
                    }
                }
            } else
            {
                if (gameType == GameType.MultiPlayer)
                {
                    nextLevelButton.SetActive(true);
                    LevelsManager.instance.levels[currentLevel].UnlockCoopLevel();
                }
                else if (gameType == GameType.SinglePlayer)
                {
                    nextLevelButton.SetActive(true);
                    LevelsManager.instance.levels[currentLevel].UnlockIndividualLevel();
                }else if (gameType == GameType.StoryMode)
                {
                    nextLevelButton.SetActive(true);
                    LevelsManager.instance.levels[currentLevel].StoryLevels(true);
                }
            }

            MenuManager.instance.PauseGame();
            EnemySpawner.instance.DestroyAllEnemies();
        }
    }

    public void CheckPowerups()
    {
        powerups++;

        powerupsTxt.text = powerups.ToString();
    }

    public PlayerController GetPlayer1()
    {
        return player1;
    }
    public PlayerController GetPlayer2()
    {
        return player2;
    }
}

public enum GameType
{
    SinglePlayer,
    MultiPlayer,
    StoryMode
}