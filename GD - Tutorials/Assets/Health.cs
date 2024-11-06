using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [Header("UI Elements")]
    [SerializeField] private Image myHealthSlider;

    //Private
    private GameManager gameManager;

    private void Start()
    {
        //REMOVER QUANDO FOR CHAMADO NO GAME MANAGER
        SetupLifes();
    }

    public void SetupLifes()
    {
        myHealthSlider.fillAmount = 1;
        gameManager = GameManager.instance;
    }

    public void TakeDamage()
    {
        currentHealth -= .1f;
        currentHealth = Mathf.Clamp(currentHealth, 0, 1);

        myHealthSlider.DOFillAmount(currentHealth, .5f).OnComplete(() =>
        {
            if (currentHealth <= 0)
            {
                gameManager.LoseGame();
            }
        });
    }

    public Image GetHealthSlider()
    {
        return myHealthSlider;
    }
}
