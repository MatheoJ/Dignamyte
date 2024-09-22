using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlastHandler : MonoBehaviour
{
    // private float delayBomb;
    private float delayChainedBomb;
    private float blastRadius;
    private float blastForce;
    private float deadZoneRadius;

    
    private GlobalBombParam globalBombParam;
    
    private void Start()
    {
        //GlobalBombParam.OnCompleteEvent += Setup();
        //Get the GlobalBombParam with tag
        globalBombParam = GameObject.FindGameObjectWithTag("BombParam").GetComponent<GlobalBombParam>();
        
        //In case the GlobalBombParam was already setup
        Setup();
    }

    private Action Setup()
    {
        // delayBomb = globalBombParam.delayBomb;
        delayChainedBomb = globalBombParam.delayChainedBomb;
        blastForce = globalBombParam.blastForce;
        blastRadius = globalBombParam.blastRadius;
        deadZoneRadius = globalBombParam.deadZoneRadius;

        return null;
    }


    public void BlastAway(Vector3 otherPosition)
    {
            BlastAway(otherPosition, blastForce, blastRadius);
    }
    
    public void BlastAway(Vector3 otherPosition, float customBlastForce, float customBlastRadius)
    {

        if (GetComponent<NavMeshAgent>() != null)
        {
            BlastAwayAI(otherPosition, customBlastForce, customBlastRadius);
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
                    
            Debug.Log("BlastAway");
                    
            if (rb != null)
            {  Debug.Log("BlastAway");
                        
                rb.AddExplosionForce(customBlastForce, otherPosition, customBlastRadius);
                rb.AddForce(new Vector3(0, customBlastForce / 2, 0));
            }    
        }
        
    }
    
    
    public void BlastAwayAI(Vector3 otherPosition, float customBlastForce, float customBlastRadius)
    {
        StartCoroutine(ApplyBlast(otherPosition, customBlastForce, customBlastRadius));
    }
    
    public IEnumerator ApplyBlast(Vector3 otherPosition, float customBlastForce, float customBlastRadius)
    {
        yield return null;

        var agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        
        rigidbody.AddExplosionForce(customBlastForce, otherPosition, customBlastRadius);
        rigidbody.AddForce(new Vector3(0, customBlastForce, 0));

        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => rigidbody.velocity.magnitude < 0.05f);
        yield return new WaitForSeconds(0.25f);

        
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;

        rigidbody.isKinematic = true;
        rigidbody.useGravity = true;

        agent.Warp(transform.position);
        agent.enabled = true;

        yield return null;

    }
    
    
}
