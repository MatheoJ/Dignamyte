using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private GameManager gameManager;
    private float lastDestinationChangeTime = 0.0f;
    
    [SerializeField]
    private float destinationChangeInterval = 1.0f;

    private float realChangeInterval;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        realChangeInterval = Random.Range(destinationChangeInterval - 0.2f*destinationChangeInterval, destinationChangeInterval + 0.2f*destinationChangeInterval);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.AreEnemiesStunned)
        {
            agent.isStopped = true;
            return;
        }else if(agent.isStopped)
        {
            agent.isStopped = false;
        }
        
        if(ShouldChangeDestination())
        {
            ChangeDestinationToPlayerPosition();
        }
        
        //IsAgentOnFallNavLink();
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
        Transform playerTransform = player.transform;
        agent.SetDestination(playerTransform.position);
    }
    
    void IsAgentOnFallNavLink()
    {
        if(agent.isOnOffMeshLink)
        {
            
            Debug.Log("Jumping");
            Vector3 directionOfNavLink = agent.currentOffMeshLinkData.endPos - agent.currentOffMeshLinkData.startPos;
            agent.enabled = false;
            Vector3 jumpDirection = new Vector3(directionOfNavLink.x, 0.0f, directionOfNavLink.z);
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.velocity=(jumpDirection.normalized * 2.0f);
        }
    }
    
    public void kill()
    {
        Destroy(gameObject);
    }
}