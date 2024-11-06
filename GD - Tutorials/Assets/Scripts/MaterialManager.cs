using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] List<MaterialSO> materials = new List<MaterialSO>();
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    Sprite circleSprite;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    WorldHolder player;

    private void Start()
    {
        player = WorldHolder.instance;
        SetButtons();
    }

    public void SetButtons()
    {
        foreach (var material in materials)
        {
            GameObject currentGO = Instantiate(buttonPrefab, buttonParent);
            Button currentButton = currentGO.GetComponent<Button>();
            buttons.Add(currentGO);

            Image buttonImage = currentButton.targetGraphic as Image;
            buttonImage.sprite = material.GetPreview();

            currentButton.interactable = !material.IsLocked();
            currentButton.transform.GetChild(1).gameObject.SetActive(material.IsLocked());

            currentButton.onClick.RemoveAllListeners();
            currentButton.onClick.AddListener(() => ChangeMaterial(material.GetMaterial()));
        }
    }

    public void ChangeMaterial(Material material)
    {
        player.SetMaterial(material);
    }
}