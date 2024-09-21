﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/BombeNum")]

public class BombeNumPWU : PoweupEffect
{


    public override void Apply(GameObject target)
    {

        target.GetComponent<CharacterMouvement>().limiteBombe += 1;
    }

}