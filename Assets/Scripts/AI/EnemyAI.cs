using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
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
    
    
    [SerializeField]
    private float _jumpDuration = 0.8f;

    public UnityEvent OnLand, OnStartJump;
    
    private bool _onNavMeshLink = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
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
        
        if (agent.isOnOffMeshLink && _onNavMeshLink == false)
        {
            StartNavMeshLinkMovement();
        }
        if (_onNavMeshLink)
        {
            FaceTarget(agent.currentOffMeshLinkData.endPos);
        }
        
        
        //IsAgentOnFallNavLink();
    }
    
    bool ShouldChangeDestination()
    {
        if(_onNavMeshLink)
        {
            return false;
        }
        if(Time.time - lastDestinationChangeTime > destinationChangeInterval)
        {
            return true;
        }
        return false;
    }
    
    void ChangeDestinationToPlayerPosition()
    {
        Transform playerTransform = player.transform;
        agent.SetDestination(playerTransform.position);
        lastDestinationChangeTime = Time.time;
    }
    
    public void kill()
    {
        Destroy(gameObject);
    }
    
    private void StartNavMeshLinkMovement()
    {
        _onNavMeshLink = true;
        NavMeshLink link = (NavMeshLink)agent.navMeshOwner;
        Spline spline = link.GetComponentInChildren<Spline>();

        PerformJump(link, spline);
    }

    private void PerformJump(NavMeshLink link, Spline spline)
    {
        bool reverseDirection = CheckIfJumpingFromEndToStart(link);
        StartCoroutine(MoveOnOffMeshLink(spline, reverseDirection));

        OnStartJump?.Invoke();
    }

    private bool CheckIfJumpingFromEndToStart(NavMeshLink link)
    {
        Vector3 startPosWorld
            = link.gameObject.transform.TransformPoint(link.startPoint);
        Vector3 endPosWorld
            = link.gameObject.transform.TransformPoint(link.endPoint);

        float distancePlayerToStart 
            = Vector3.Distance(agent.transform.position, startPosWorld);
        float distancePlayerToEnd 
            = Vector3.Distance(agent.transform.position, endPosWorld);


        return distancePlayerToStart > distancePlayerToEnd;
    }
    
    private IEnumerator MoveOnOffMeshLink(Spline spline, bool reverseDirection)
    {
        float currentTime = 0;
        Vector3 agentStartPosition = agent.transform.position;

        while (currentTime < _jumpDuration)
        {
            currentTime += Time.deltaTime;

            float amount = Mathf.Clamp01(currentTime / _jumpDuration);
            amount = reverseDirection ? 1 - amount : amount;

            agent.transform.position =
                reverseDirection ?
                    spline.CalculatePositionCustomEnd(amount, agentStartPosition)
                    : spline.CalculatePositionCustomStart(amount, agentStartPosition);

            yield return new WaitForEndOfFrame();
        }

        agent.CompleteOffMeshLink();

        OnLand?.Invoke();
        yield return new WaitForSeconds(0.1f);
        _onNavMeshLink = false;
    }
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation 
            = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation 
            = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}