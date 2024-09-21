using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{

    public int health = 1;

    public bool invincible;


   
    // Start is called before the first frame update
    void Start()
    {
        invincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(invincible == true)
        {
            StartCoroutine(InvincibleTimer());


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
       
            if (collision.gameObject.tag == "Ennemi")
            {
                 Kill();

            }
        
      
    }




    public void Kill()
    {
        if (invincible != true)
        {

        }


    }


    private IEnumerator InvincibleTimer()
    {
      
            yield return new WaitForSeconds(5f);
            invincible = false;
        
    }

}
