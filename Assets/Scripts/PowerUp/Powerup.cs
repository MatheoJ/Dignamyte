﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PoweupEffect poweupEffect;
    
    public float timeBeforeDestroy = 20.0f;

    public float volume = 5;
    public AudioSource pickUpPWU;
   // public GameManager perso;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Play the sound in the ui manager
            GameObject.FindGameObjectWithTag("InGameUIManager").GetComponent<InGameUIManager>().powerUpSound.Play();
            
            pickUpPWU.PlayOneShot(pickUpPWU.clip, volume);
            // Destroy the parent of the object as well
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            
            Destroy(gameObject);
            poweupEffect.Apply(collision.gameObject);
        }
       
    }

    private void Update()
    {
        StartCoroutine(PowerupGone());
    }

    private IEnumerator PowerupGone()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        
        // Destroy the parent of the object as well
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        
        Destroy(gameObject);
    }

}
