using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InGameUIManager inGameUIManager;

    [SerializeField] private int leaderBoardScene;
        
    
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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
       SceneManager.LoadScene(leaderBoardScene);
    }
}
