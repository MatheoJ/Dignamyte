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

    private void Start()
    {
        GlobalBombParam.OnCompleteEvent += Setup();
        
        //In case the GlobalBombParam was already setup
        Setup();
    }

    private Action Setup()
    {
        delayBomb = GlobalBombParam.Instance.delayBomb;
        delayChainedBomb = GlobalBombParam.Instance.delayChainedBomb;
        blastForce = GlobalBombParam.Instance.blastForce;
        blastRadius = GlobalBombParam.Instance.blastRadius;
        deadZoneRadius = GlobalBombParam.Instance.deadZoneRadius;

        return null;
    }


    public void BlastAway(Vector3 otherPosition)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            rb.AddExplosionForce(blastForce, otherPosition, blastRadius);
            rb.AddForce(new Vector3(0, blastForce / 2, 0));
        }
    }
}
