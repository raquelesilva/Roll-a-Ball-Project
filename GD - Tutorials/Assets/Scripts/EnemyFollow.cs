using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] Transform player;
    private NavMeshAgent navMeshAgent;

    [SerializeField]float enemyStartSpeed = 2.5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = GameManager.instance.GetPlayer1().transform;
        }

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);

        }
    }

    public void SetSpeed(float enemySpeedIncrement)
    {
        navMeshAgent.speed *= enemySpeedIncrement;
    }
}