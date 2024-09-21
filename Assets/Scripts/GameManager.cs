using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InGameUIManager inGameUIManager;
    
    private int killCount = 0;
    
    private float timeSinceGameStart;
    
    private bool areEnemiesStunned = false; 
    
    private float stuntTime = 5.0f;
    
    private float timeSinceStuntStart = 0.0f;
    
    
    public bool AreEnemiesStunned
    {
        get
        {
            return areEnemiesStunned;
        }
        set
        {
            areEnemiesStunned = value;
            if(areEnemiesStunned)
            {
                timeSinceStuntStart = Time.time;
            }
        }
    }
    
    void Start()
    {
        timeSinceGameStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyStunStatus();
    }
    
    public void IncrementKillCount()
    {
        killCount++;
        inGameUIManager.UpdateKillCount(killCount);
    }
    
    public void setStuntTime(float time)
    {
        stuntTime = time;
    }
    
    private void UpdateEnemyStunStatus()
    {
        if(areEnemiesStunned)
        {
            if(Time.time - timeSinceStuntStart > stuntTime)
            {
                areEnemiesStunned = false;
            }
        }
    }

    public void EndGame()
    {
       //TODO change scene and pass the score to it
       
    }
}
