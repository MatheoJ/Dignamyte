using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private float lastDestinationChangeTime = 0.0f;
    
    [SerializeField]
    private float destinationChangeInterval = 1.0f;

    private float realChangeInterval;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        realChangeInterval = Random.Range(destinationChangeInterval - 0.2f*destinationChangeInterval, destinationChangeInterval + 0.2f*destinationChangeInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldChangeDestination())
        {
            ChangeDestinationToPlayerPosition();
        }
    }
    
    bool ShouldChangeDestination()
    {
        if(Time.time - lastDestinationChangeTime > destinationChangeInterval)
        {
            lastDestinationChangeTime = Time.time;
            return true;
        }
        return false;
    }
    
    void ChangeDestinationToPlayerPosition()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.SetDestination(player.position);
    }
}
