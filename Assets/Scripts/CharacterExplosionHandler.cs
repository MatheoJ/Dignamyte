using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExplosionHandler : MonoBehaviour
{

    [SerializeField] private BombParam bombParam;
    
    public void ApplyForce(Vector3 otherPosition)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Apply force to the rigidbody
            rb.AddExplosionForce(bombParam.blastForce, otherPosition, bombParam.blastRadius);
        }
    }
}
