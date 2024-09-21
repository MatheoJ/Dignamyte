using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExplosionHandler : MonoBehaviour
{
    public void ApplyForce(Vector3 otherPosition, BombParam bombParam)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddExplosionForce(bombParam.blastForce, otherPosition, bombParam.blastRadius);
            rb.AddForce(new Vector3(0, bombParam.blastForce / 2, 0));
        }
    }
}
