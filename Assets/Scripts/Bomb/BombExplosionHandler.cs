using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class BombExplosionHandler : MonoBehaviour
{
    
    public float timeSelfExplosion;
    public float timeChainedExplosion;
    public float blastRadius;
    public float deadzoneRadius;
    public float blastForce;
    
    [SerializeField]
    private int playerLayer;
    [SerializeField]
    private int enemyLayer;
    [SerializeField] 
    private int bombLayer;
    
  
    
    [SerializeField] private LayerMask layerMask;
    
    
    private bool _exploded;

    private void Start()
    {
        HandleExplosion(timeSelfExplosion);
    }

    public void HandleExplosion(float time)
    {
        StartCoroutine(DelayCoroutine(time));
    }

    private IEnumerator DelayCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Explode();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Explode()
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
                
            ExploseOther();    
            
            Destroy(this.gameObject);
        }
        
        
    }

    private void ExploseOther()
    {
        var affectedCollider = Physics.OverlapSphere(transform.position, blastRadius, layerMask);
                
        foreach (var collider in affectedCollider)    
        {
            //TODO apply the explosion to them
            var targetLayerMask = collider.gameObject.layer;
                
                
            if (targetLayerMask == playerLayer )
            {
                CustomLogger.Log("Player detected");
                collider.gameObject.GetComponent<CharacterExplosionHandler>()?.ApplyForce(transform.position, new BombParam()
                {
                    blastForce = blastForce,
                    blastRadius = blastRadius
                }); 
                continue;
            }

            if (targetLayerMask == enemyLayer)
            {
                 CustomLogger.Log("Player detected");
                 collider.gameObject.GetComponent<CharacterExplosionHandler>()?.ApplyForce(transform.position, new BombParam()
                 {
                     blastForce = blastForce,
                     blastRadius = blastRadius
                 }); 
                 continue;               
                 
            }
                           
            if (targetLayerMask == bombLayer)
            {
                if (collider.gameObject != gameObject)
                {
                    CustomLogger.Log("Bomb detected");
                    collider.gameObject.GetComponent<BombExplosionHandler>()?.HandleExplosion(timeChainedExplosion);
                }
                continue;
            }
                            
            //Should never end up here or else we have a problem
            Assert.True(false);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.5f, 0, 0.5f);
        Gizmos.DrawSphere(gameObject.transform.position, blastRadius);
    }
}
