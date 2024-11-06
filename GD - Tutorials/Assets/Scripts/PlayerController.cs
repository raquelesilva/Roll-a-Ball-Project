using DG.Tweening;
using System.Collections;
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

    [SerializeField] private Image impact;

    public SinglePlayerManager singlePlayerManager;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        singlePlayerManager = SinglePlayerManager.instance;
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
            singlePlayerManager.CheckPoints();
        }

        if (other.CompareTag("Enemy"))
        {
            singlePlayerManager.CheckLife();

            StartCoroutine(FlashRed());
        }

        if (other.CompareTag("Powerup"))
        {
            singlePlayerManager.powerups++;

            other.GetComponent<Downgrades>().SetDowngrade();
        }

        if (other.CompareTag("Downgrade"))
        {
            other.GetComponent<Downgrades>().SetDowngrade();
        }
    }

    private IEnumerator FlashRed()
    {
        impact.DOColor(new Color(255, 255, 255, 1), .5f);
        yield return new WaitForSeconds(.5f);
        impact.DOColor(new Color(255, 255, 255, 0), .5f);
        yield return new WaitForSeconds(.5f);
        impact.DOColor(new Color(255, 255, 255, 1), .5f);
        yield return new WaitForSeconds(.5f);
        impact.DOColor(new Color(255, 255, 255, 0), .5f);
        yield return new WaitForSeconds(.5f);
    }
}