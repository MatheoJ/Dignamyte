using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour
{
    public Text score;
    public Text time;
    public string gameScene;
   // public Text inTime;

    int scoreCount;
    float timeCount;
    public 
    // Start is called before the first frame update
    void Start()
    {
        scoreCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().killCount;
       timeCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().totaltime;

        int minutes = (int)timeCount / 60;
        int seconds = (int)timeCount % 60;
       // time.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        Debug.Log("Current Kill Count:" + scoreCount);
        score.text = ""+scoreCount;
        time.text =  minutes.ToString("00") + ":" + seconds.ToString("00"); ;
        Debug.Log("Current Kill Count:" + (int)timeCount);

    }
    
    void Update()
    {
        //Restart if R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(gameScene);
        }
    }

}
