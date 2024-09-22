using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Invincible")]

public class InvinciblePWU : PoweupEffect
{

    public override void Apply(GameObject target)
    {
        target.GetComponent<CharacterHealth>().invincible = true;
    }



}
