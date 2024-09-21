using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class ExplosionHandler : MonoBehaviour
{
    
    private float delayBomb;
    private float delayChainedBomb;
    private float blastRadius;
    private float blastForce;
    private float deadZoneRadius;

    [SerializeField] private LayerMask layerMask;

    private bool _exploded;

    private const int PlayerLayer = 11;
    private const int EnemyLayer = 10;
    private const int BombLayer = 9;


    private void Start()
    {
        delayBomb = GlobalBombParam.Instance.delayBomb;
        delayChainedBomb = GlobalBombParam.Instance.delayChainedBomb;
        blastForce = GlobalBombParam.Instance.blastForce;
        blastRadius = GlobalBombParam.Instance.blastRadius;
        deadZoneRadius = GlobalBombParam.Instance.deadZoneRadius;
    }

    
    
    public void Explode()
    {
        ExplodeOther();
        ExplodeSelf();
    }

    private void ExplodeSelf()
    {
        if (!_exploded)
        {
            _exploded = true;
            //Explose Self
            var objectRenderer = GetComponent<Renderer>();
                        
            // Get the current material color
            Color currentColor = objectRenderer.material.color;
            Debug.Log("Current Color: " + currentColor);
                        
            // Change the RGB values (for example, increase red by 0.1)
            Color newColor = new Color(1, 0, 0);
            objectRenderer.material.color = newColor;
                                
            Destroy(this.gameObject);    
        }
        
    }

    private void ExplodeOther()
    {
        var affectedCollider = Physics.OverlapSphere(transform.position, blastRadius, layerMask);
                        
        foreach (var collider in affectedCollider)    
        {
            //TODO apply the explosion to them
            var targetLayerMask = collider.gameObject.layer;
                        
                        
            if (targetLayerMask == PlayerLayer)
            {
                CustomLogger.Log("Player detected");
                collider.gameObject.GetComponent<BlastHandler>()?.BlastAway(transform.position);
                continue;
            }
        
            if (targetLayerMask == EnemyLayer)
            {
                CustomLogger.Log("Player detected");
                collider.gameObject.GetComponent<CharacterExplosionHandler>()?.ApplyForce(transform.position, new BombParam()
                {
                    blastForce = blastForce,
                    blastRadius = blastRadius
                }); 
                continue;               
                         
            }
                                   
            if (targetLayerMask == BombLayer)
            {
                if (collider.gameObject != gameObject)
                {
                    CustomLogger.Log("Bomb detected");
                    collider.gameObject.GetComponent<Countdown>()?.StartCountDown(delayChainedBomb);
                }
                continue;
            }
                                    
            //Should never end up here or else we have a problem
            Assert.True(false);
        }
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.5f, 0, 0.5f);
        Gizmos.DrawSphere(gameObject.transform.position, blastRadius);
    }
    
    
}
