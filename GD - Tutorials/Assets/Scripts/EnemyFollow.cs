using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    private NavMeshAgent navMeshAgent;

    [SerializeField]float enemySpeed = 2.5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = PlayerController.instance.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}