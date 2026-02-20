using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class ObjectRunAway : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public bool playerSpotted;
    private BoxCollider boxCollider;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSpotted)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            Vector3 oppositeDirection = transform.position - directionToPlayer;
            agent.SetDestination(oppositeDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerSpotted)
            {
                player = other.gameObject.transform;
                playerSpotted = true;
                boxCollider.size = new Vector3(3, boxCollider.size.y, 3);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
