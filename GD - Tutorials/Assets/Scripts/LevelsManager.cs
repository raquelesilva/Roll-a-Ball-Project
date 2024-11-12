using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;

    [SerializeField] public List<Level> levels = new List<Level>();
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    WorldHolder player;

    public MenuManager menuManager;
    public static LevelsManager instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        menuManager = MenuManager.instance;
        player = WorldHolder.instance;
    }

    public void SetButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }

        foreach (var level in levels)
        {
            GameObject currentGO = Instantiate(buttonPrefab, buttonParent);
            Button currentButton = currentGO.GetComponent<Button>();
            buttons.Add(currentGO);

            currentGO.GetComponentInChildren<TextMeshProUGUI>().text = level.GetLevel().ToString();

            if (GameManager.instance.gameType == GameType.SinglePlayer)
            {
                title.text = "Single Player";
                currentButton.interactable = level.GetIndividualState();
                currentButton.transform.GetChild(1).gameObject.SetActive(!level.GetIndividualState());

                if (level.GetSingleStars() > 0)
                {
                    Transform starsParent = currentButton.transform.GetChild(2);

                    for (int i = 0; i < level.GetSingleStars(); i++)
                    {
                        starsParent.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
            else if (GameManager.instance.gameType == GameType.MultiPlayer)
            {
                title.text = "Multiplayer";
                currentButton.interactable = level.GetCoopState();
                currentButton.transform.GetChild(1).gameObject.SetActive(!level.GetCoopState());

                if (level.GetCoopStars() > 0)
                {
                    Transform starsParent = currentButton.transform.GetChild(2);

                    for (int i = 0; i < level.GetCoopStars(); i++)
                    {
                        starsParent.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }

            currentButton.onClick.AddListener(() => PlayLevel(level));
        }
    }

    public void PlayLevel(Level level)
    {
        menuManager.mainMenuWindow.SetActive(false);
        menuManager.levelsMenuWindow.SetActive(false);
        menuManager.materialsMenuWindow.SetActive(false);
        menuManager.gameMenu.SetActive(true);

        if (player == null)
        {
            player = WorldHolder.instance;
        }

        player.SetCurrentLevel(level);

        GameManager.instance.SetWorld();
    }

    public void PlayStory()
    {
        for (int i = levels.Count - 1; i >= 0; i--)
        {
            Level currentLevel = levels[i];

            if (currentLevel.GetStoryState())
            {
                PlayLevel(currentLevel);
                return;            
            }
        }
    }
}