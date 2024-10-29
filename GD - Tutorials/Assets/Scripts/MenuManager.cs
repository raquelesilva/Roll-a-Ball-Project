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
        playerController.GetComponent<PlayerInput>().enabled = false;
        playerController.rb.Sleep();

        List<GameObject> enemies = EnemySpawner.instance.enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Rigidbody>().Sleep();
        }
    }

    public void UnpauseGame()
    {
        playerController.GetComponent<PlayerInput>().enabled = true;
        playerController.rb.WakeUp();

        List<GameObject> enemies = EnemySpawner.instance.enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Rigidbody>().WakeUp();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}