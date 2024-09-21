using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Range")]

public class RangePWU : PoweupEffect
{

    public float amount;
   

    public override void Apply(GameObject target)
    {
        GlobalBombParam.Instance.blastRadius += amount;
    }
}
