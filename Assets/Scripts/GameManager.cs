using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InGameUIManager inGameUIManager;

    [SerializeField] private int leaderBoardScene;

    [SerializeField] private AudioSource music;
        
    
    public int killCount = 0;
    
    public float timeSinceGameStart;
    
    private bool areEnemiesStunned = false; 
    
    private float stuntTime = 5.0f;
    
    private float timeSinceStuntStart = 0.0f;

    public float totaltime;


    public Text timeTextCount;
    
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
                inGameUIManager.startFreezeClock(stuntTime);
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        music.Play();
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
        
        

        totaltime = Time.time - timeSinceGameStart;

        SceneManager.LoadScene(leaderBoardScene);
        
    }
}
