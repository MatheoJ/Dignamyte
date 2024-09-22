using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Blast")]
public class BlastPWU : PoweupEffect
{
    public GameObject blastFx;

    public LayerMask layerMask;
    public LayerMask obstacleLayerMask;
    
    
    public float blastRadius;
    public float blastForce;

    private const int PlayerLayer = 11;
    private const int EnemyLayer = 10;
    private const int BombLayer = 9;
    
    
    public override void Apply(GameObject target)
    {
        //Instantiate vfx
        var obj = Instantiate(blastFx);
        obj.transform.position = target.transform.position;
        
        //Apply blast effect
        var potentialBlastInstance = Physics.OverlapSphere(target.transform.position, blastRadius, layerMask);

        foreach (var collider in potentialBlastInstance)
        {
            var targetLayerMask = collider.gameObject.layer;
                                        
            if (targetLayerMask == PlayerLayer && collider.gameObject != target)
            {
                //Do Nothing
                continue;
            }
                        
            if (targetLayerMask == EnemyLayer)
            {
                if (!IsObstructed(target.transform.position, collider.gameObject.transform.position))
                {
                    collider.gameObject.GetComponent<BlastHandler>()?.BlastAway(target.transform.position, blastForce, blastRadius);
                }
                continue;               
                                         
            }
                                                   
            if (targetLayerMask == BombLayer)
            {
                //Do Nothing
                
                continue;
            }
                                                    
            //Should never end up here or else we have a problem
        }
    }
    
    private bool IsObstructed(Vector3 originPosition, Vector3 targetPosition)
    {
        var direction = (targetPosition - originPosition).normalized; // Direction to target
        float distance = Vector3.Distance(originPosition, targetPosition); // Distance to target
    
        // Perform the raycast
        if (Physics.Raycast(originPosition, direction, out RaycastHit hitInfo, distance, obstacleLayerMask))
        {
            return true;
        }
        return false;
    }
}
