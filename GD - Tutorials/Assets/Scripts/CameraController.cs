using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Quaternion initialRotation;

    [SerializeField] bool followPlayer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameManager.instance.GetPlayer1().gameObject;

        offset = transform.position - player.transform.position;

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (followPlayer)
            {
                transform.position = player.transform.position + offset;
                transform.LookAt(player.transform);
            }
        }
    }

    public void SetFollow(bool follow)
    {
        followPlayer = follow;
        GotoInitialPos();
    }

    public void GotoInitialPos()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}