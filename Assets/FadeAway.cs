using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeAway : MonoBehaviour
{
   public DecalProjector decal;
   
   public float startValue = 1f; // Starting value
   public float endValue = 0f; // Target value
   public float duration = 2f; // Duration of the lerp

   private float elapsedTime = 0f;
   private bool isLerping = false;

   private void Start()
   {
      StartLerp();
   }

   private void Update()
   {
      if (isLerping)
      {
         // Update the elapsed time
         elapsedTime += Time.deltaTime;

         // Calculate the progress
         float t = Mathf.Clamp01(elapsedTime / duration);

         // Lerp the value
         float currentValue = Mathf.Lerp(startValue, endValue, t);

         decal.fadeFactor = currentValue;

         // Stop lerping once we reach the end
         if (t >= 1f)
         {
            isLerping = false;
         }
      }
   }

   public void StartLerp()
   {
      elapsedTime = 0f; // Reset elapsed time
      isLerping = true; // Start lerping
   }
   
}
