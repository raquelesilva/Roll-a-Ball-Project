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

    [SerializeField] Player playerSO;

    private void OnEnable()
    {
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

            if (!level.GetState())
            {
                currentButton.interactable = false;
                currentButton.transform.GetChild(1).gameObject.SetActive(true);
            }

            currentButton.onClick.AddListener(() => PlayLevel(level));
        }
    }

    public void PlayLevel(Level level)
    {
        playerSO.SetCurrentLevel(level);

        SceneManager.LoadScene(1);
    }
}