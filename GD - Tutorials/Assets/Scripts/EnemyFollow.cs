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
            if(GameManager.instance.gameType == GameType.SinglePlayer || GameManager.instance.gameType == GameType.StoryMode)
            {
                player = GameManager.instance.GetPlayer1().transform; 
            }
            else
            {
                int randomFollow = Random.Range(0, 10);

                if (randomFollow % 2 == 0)
                {
                    player = GameManager.instance.GetPlayer1().transform; 
                }
                else
                {
                    player = GameManager.instance.GetPlayer2().transform;
                }
            }
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