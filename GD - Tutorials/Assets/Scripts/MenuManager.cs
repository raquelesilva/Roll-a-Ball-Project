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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SinglePlayerManager.instance.SetWorld();
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

        mainMenuWindow.SetActive(true);
        levelsMenuWindow.SetActive(false);
        materialsMenuWindow.SetActive(false);
        gameMenu.SetActive(false);
    }


    public void LeaveGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}