using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Speed")]

public class SpeedPWU : PoweupEffect
{

    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<CharacterMouvement>()._speed += amount;
    }




}
