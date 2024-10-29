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

    public SinglePlayerManager singlePlayerManager;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody>();
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            singlePlayerManager.CheckLife();
        }
    }
}