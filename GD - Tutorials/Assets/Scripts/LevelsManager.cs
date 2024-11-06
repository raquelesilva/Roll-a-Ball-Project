using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
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
        SetButtons();
    }

    public void SetButtons()
    {
        foreach (var level in levels)
        {
            GameObject currentGO = Instantiate(buttonPrefab, buttonParent);
            Button currentButton = currentGO.GetComponent<Button>();
            buttons.Add(currentGO);

            currentGO.GetComponentInChildren<TextMeshProUGUI>().text = level.GetLevel().ToString();

            currentButton.interactable = level.GetState();
            currentButton.transform.GetChild(1).gameObject.SetActive(!level.GetState());

            currentButton.onClick.AddListener(() => PlayLevel(level));
        }
    }

    public void PlayLevel(Level level)
    {
        menuManager.mainMenuWindow.SetActive(false);
        menuManager.levelsMenuWindow.SetActive(false);
        menuManager.materialsMenuWindow.SetActive(false);
        menuManager.gameMenu.SetActive(true);

        player.SetCurrentLevel(level);

        GameManager.instance.SetWorld();
    }
}