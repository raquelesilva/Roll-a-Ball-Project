using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    PlayerController playerController;
    public static MenuManager instance;

    [Header("Windows")]
    [SerializeField] public GameObject mainMenuWindow;
    [SerializeField] public GameObject levelsMenuWindow;
    [SerializeField] public GameObject materialsMenuWindow;
    [SerializeField] public GameObject gameMenu;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerController = PlayerController.instance;

        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        GameManager.instance.SetWorld();
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1;
        
        if (GameManager.instance.currentWorld != null)
        {
            Destroy(GameManager.instance.currentWorld.gameObject);
            GameManager.instance.currentWorld = null;
        }

        mainMenuWindow.SetActive(true);
        levelsMenuWindow.SetActive(false);
        materialsMenuWindow.SetActive(false);
        gameMenu.SetActive(false);
    }

    public void GotoNextLevel()
    {
        WorldHolder.instance.SetCurrentLevelINT(WorldHolder.instance.GetCurrentLevelINT() + 1);
        LevelsManager.instance.PlayLevel(WorldHolder.instance.GetCurrentLevel());
    }

    public void LeaveGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}