using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurrySelf : MonoBehaviour
{
   public GameObject burryFX;

   public float rotationSpeed;

   public float startValue = 1f; // Starting value
   public float endValue = 0f; // Target value
   public float duration = 2f; // Duration of the lerp
    
   private float elapsedTime = 0f;
   private bool isLerping = false;
    
    
   public void StartLerp()
   {
      elapsedTime = 0f; // Reset elapsed time
      isLerping = true; // Start lerping

      var obj = Instantiate(burryFX);
      obj.transform.position = transform.position;

      var agent = GetComponent<NavMeshAgent>();

      if (agent != null)
      {
         agent.enabled = false;
      }

      //Put on the side the character
      // StartCoroutine(RotateCharacter());
      transform.Rotate(rotationSpeed, 0, 0);
      
      //Hide the character in the ground for a duration
      StartCoroutine(Lerp());
   }

   public IEnumerator Lerp()
   {
      while (elapsedTime < duration)
      {
         if (isLerping)
         {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;
                 
            // Calculate the progress
            float t = Mathf.Clamp01(elapsedTime / duration);
                 
            // Lerp the value
            float currentValue = Mathf.Lerp(startValue, endValue, t);
                 
                
            //TODO move the player on the ground
            var vector3 = transform.position;
            vector3.y -= currentValue * 0.2f;
            transform.position = vector3;
                
            // Stop rping once we reach the end
            if (t >= 1f)
            {
               isLerping = false;
            }
         }
         yield return null;
      }
      
      Destroy(gameObject);
   }

   private IEnumerator RotateCharacter()
   {
      while (transform.rotation.x < 90)
      {
         transform.Rotate(rotationSpeed, 0, 0);
         yield return null;
      }
   }
}
