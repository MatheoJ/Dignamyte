using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBombParam : MonoBehaviour
{
    public static GlobalBombParam Instance { get; set; }
    
    [SerializeField] public float delayBomb;
    [SerializeField] public float delayChainedBomb;
    [SerializeField] public float blastRadius;
    [SerializeField] public float blastForce;
    [SerializeField] public float deadZoneRadius;
    
    private void Start()
    {
        Instance = this;
    }
    
}