using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private LayerMask obstacleMask;

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
        var deadInstance = Physics.OverlapSphere(transform.position, deadZoneRadius, layerMask);
        var potentialBlastInstance = Physics.OverlapSphere(transform.position, blastRadius, layerMask);

        var blastInstance = potentialBlastInstance.Where(x => !deadInstance.Contains(x));
                        
        //Check for each object affected in the blast and apply some logic to them based on their type
        foreach (var collider in blastInstance)    
        {
            var targetLayerMask = collider.gameObject.layer;
                        
            if (targetLayerMask == PlayerLayer)
            {
                if (!IsObstructed(gameObject.transform.position, collider.gameObject.transform.position))
                {
                    collider.gameObject.GetComponent<BlastHandler>()?.BlastAway(transform.position);
                }
                continue;
            }
        
            if (targetLayerMask == EnemyLayer)
            {
                if (!IsObstructed(gameObject.transform.position, collider.gameObject.transform.position))
                {
                    collider.gameObject.GetComponent<BlastHandler>()?.BlastAway(transform.position);
                }
                continue;               
                         
            }
                                   
            if (targetLayerMask == BombLayer)
            {
                if (collider.gameObject != gameObject && !IsObstructed(gameObject.transform.position, collider.gameObject.transform.position))
                {
                    collider.gameObject.GetComponent<Countdown>()?.StartCountDown(delayChainedBomb);
                }
                continue;
            }
                                    
            //Should never end up here or else we have a problem
            Assert.True(false);
        }

        //Check for each object affected by the deadzone and apply some logic to them based on their type
        foreach (var collider in deadInstance)
        {
            var targetLayerMask = collider.gameObject.layer;
                                    
            if (targetLayerMask == PlayerLayer)
            {
                //TODO kill player
                continue;
            }
                    
            if (targetLayerMask == EnemyLayer)
            {
                //TODO kill enemy
                continue;               
            }
                                               
            if (targetLayerMask == BombLayer)
            {
                //Nothing to do here
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

    private bool IsObstructed(Vector3 originPosition, Vector3 targetPosition)
    {
        var direction = (targetPosition - originPosition).normalized; // Direction to target
        float distance = Vector3.Distance(originPosition, targetPosition); // Distance to target

        // Perform the raycast
        if (Physics.Raycast(originPosition, direction, out RaycastHit hitInfo, distance, obstacleMask))
        {
            return true;
        }
        return false;
    }
    
    
}
