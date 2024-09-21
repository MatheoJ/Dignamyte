using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/BombParam")]
public class BombParam : ScriptableObject
{
    public float timeSelfExplosion;
    public float timeChainedExplosion;
    public float blastRadius;
    public float blastForce;
}
