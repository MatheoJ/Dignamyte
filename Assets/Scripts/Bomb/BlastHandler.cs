using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlastHandler : MonoBehaviour
{
    public void BlastAway(Vector3 otherPosition, float customBlastForce, float customBlastRadius)
    {

        if (GetComponent<NavMeshAgent>() != null)
        {
            BlastAwayAI(otherPosition, customBlastForce, customBlastRadius);
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
                    
            if (rb != null)
            {  
                rb.AddExplosionForce(customBlastForce, otherPosition, customBlastRadius);
                rb.AddForce(new Vector3(0, customBlastForce / 2, 0));
            }    
        }
        
    }
    
    
    public void BlastAwayAI(Vector3 otherPosition, float customBlastForce, float customBlastRadius)
    {
        StartCoroutine(ApplyBlast(otherPosition, customBlastForce, customBlastRadius));
    }
    
    public void BlastAwayPlayer(Vector3 otherPosition, float customBlastForce, float customBlastRadius)
    {
        StartCoroutine(ApplyBlastPlayer(otherPosition, customBlastForce, customBlastRadius));
    }

    public IEnumerator ApplyBlastPlayer(Vector3 otherPosition, float customBlastForce, float customBlastRadius)
    {
        yield return null;

        var playerController = GetComponent<CharacterMouvement>();

        playerController.enabled = false;
 
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
         
        rigidbody.AddExplosionForce(customBlastForce, otherPosition, customBlastRadius);
        rigidbody.AddForce(new Vector3(0, customBlastForce / 2, 0));
 
        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => rigidbody.velocity.magnitude < 0.2f);
        yield return new WaitForSeconds(0.25f);

        playerController.enabled = true;
        
        yield return null;       
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
        rigidbody.AddForce(new Vector3(0, customBlastForce / 2, 0));

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
