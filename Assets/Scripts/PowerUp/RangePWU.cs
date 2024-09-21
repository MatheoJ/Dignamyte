using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Range")]

public class RangePWU : PoweupEffect
{

    public float amount;
    [SerializeField] private GameObject prefab;

    public override void Apply(GameObject target)
    {
        target = prefab;
        target.GetComponent<GlobalBombParam>().blastRadius += amount;
    }
}
