using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastHandler : MonoBehaviour
{
    private float delayBomb;
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
        delayBomb = globalBombParam.delayBomb;
        delayChainedBomb = globalBombParam.delayChainedBomb;
        blastForce = globalBombParam.blastForce;
        blastRadius = globalBombParam.blastRadius;
        deadZoneRadius = globalBombParam.deadZoneRadius;

        return null;
    }


    public void BlastAway(Vector3 otherPosition)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        Debug.Log("BlastAway");
        
        if (rb != null)
        {  Debug.Log("BlastAway");
            
            rb.AddExplosionForce(blastForce, otherPosition, blastRadius);
            rb.AddForce(new Vector3(0, blastForce / 2, 0));
        }
    }
}
