using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    PlayerController playerController;
    public static MenuManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerController = PlayerController.instance;
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
    }

    public void LeaveGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}