using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeAway : MonoBehaviour
{
   public DecalProjector decal;
   public float removeValue;
   

   public void Start()
   {
      StartCoroutine(Updatevalue());
   }

   public IEnumerator Updatevalue()
   {
      while (decal.fadeFactor > 0)
      {
         decal.fadeFactor -= removeValue;
         yield return null;
      }
   }
      
}
