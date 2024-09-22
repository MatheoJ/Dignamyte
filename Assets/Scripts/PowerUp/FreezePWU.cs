using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Freeze")]
public class FreezePWU : PoweupEffect
{

    public override void Apply(GameObject target)
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AreEnemiesStunned = true;
        // target.GetComponent<>().invincible = true;
    }


}
