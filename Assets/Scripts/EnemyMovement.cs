using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isActive = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false; // Disable movement initially
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && player != null)
        {    
            agent.SetDestination(player.position);
        }

    }
    
    public void ActivateEnemy()
    {
        isActive = true;
        agent.enabled = true;
    }
    
}
