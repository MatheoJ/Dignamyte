using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Force")]

public class ForcePWU : PoweupEffect
{
    public override void Apply(GameObject target)
    {

        target.GetComponent<CharacterHealth>().invincible = true;
    }

}
