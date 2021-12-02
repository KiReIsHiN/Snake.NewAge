
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIRun : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject player;
    public float enemyDistanceRun;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < enemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPosition = transform.position + dirToPlayer;

            _agent.SetDestination(newPosition);
        }
    }
}
