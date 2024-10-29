using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody rb;

    [Header("Movement")]
    private float movementX;
    private float movementY;
    [SerializeField] float speed = 0;

    [SerializeField] int points = 0;
    [SerializeField] float life = 100;
    [SerializeField] Transform pickupObjsParent;
    [SerializeField] List<GameObject> pickupObjs;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI pointsTxt;
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject loseWindow;
    [SerializeField] Image lifeBar;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;

        transform.position = Vector3.up;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pointsTxt.text = "Points: " + points.ToString();
        lifeBar.fillAmount = life / 100;

        if (pickupObjsParent != null)
        {
            pickupObjs.Clear();
            for (int i = 0; i < pickupObjsParent.childCount; i++)
            {
                pickupObjs.Add(pickupObjsParent.GetChild(i).gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            CheckPoints();
        }

        if (other.CompareTag("Enemy"))
        {
            life -= 10;
            lifeBar.fillAmount = life / 100;

            if (life <= 0)
            {
                life = 0;
                loseWindow.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void CheckPoints()
    {
        points += 5;

        pointsTxt.text = "Points: " + points.ToString();

        if (points >= pickupObjs.Count * 5)
        {
            winWindow.SetActive(true);

            MenuManager.instance.PauseGame();

            EnemySpawner.instance.DestroyAllEnemies();
        }
    }
}