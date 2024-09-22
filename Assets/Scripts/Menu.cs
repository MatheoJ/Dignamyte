using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public string gameScene;
    public string creditScene;
    
    public void PlayGame()
    {
        var obj = GameObject.FindGameObjectWithTag("GameManager");
        if (obj != null)
        {
            //Reset the GameManager when starting a new game
            Destroy(obj);
        }

        SceneManager.LoadScene(gameScene);
    }

    public void ViewCredit()
    {
        SceneManager.LoadScene(creditScene);
    }
    
}
