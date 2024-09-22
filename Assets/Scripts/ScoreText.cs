using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text score;

    int scoreCount;
    public 
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        scoreCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().killCount;
        score.text = "Kill Count : " + scoreCount;
    }
}
