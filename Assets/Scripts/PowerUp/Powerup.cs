using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PoweupEffect poweupEffect;

    public AudioSource cardPick;
    public float volume;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cardPick.PlayOneShot(cardPick.clip, volume);
            Destroy(gameObject);
            poweupEffect.Apply(collision.gameObject);

        }
       
    }

    private void Update()
    {
        //StartCoroutine(PowerupGone());
    }

    private IEnumerator PowerupGone()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
