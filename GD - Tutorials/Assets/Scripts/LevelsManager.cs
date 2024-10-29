using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] List<Level> levels = new List<Level>();
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    Player player;

    private void Start()
    {
        player = Player.instance;
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
        player.SetCurrentLevel(level);

        SceneManager.LoadScene(1);
    }
}