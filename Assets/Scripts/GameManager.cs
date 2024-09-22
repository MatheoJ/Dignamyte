using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InGameUIManager inGameUIManager;

    [SerializeField] private string leaderBoardScene;

    [SerializeField] private AudioSource music;
    
        
    [SerializeField] private AudioSource deadSound;
    [SerializeField] private float timeBeforeChangingScene;
    
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
                inGameUIManager?.startFreezeClock(stuntTime);
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
        deadSound.Stop();
        timeSinceGameStart = Time.time;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        inGameUIManager.gameObject.SetActive(true);
        
    }

    public void StopGame()
    {
        Time.timeScale = 0;
        //Hide in game UI
        inGameUIManager.gameObject.SetActive(false);
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
        deadSound.PlayOneShot(deadSound.clip);
        music.Stop();
        
        totaltime = Time.time - timeSinceGameStart;
        StartCoroutine(ChangeScene());
        
        //Stop everything
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMouvement>().enabled = false;
        

        AreEnemiesStunned = true;


    }


    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(timeBeforeChangingScene);
        SceneManager.LoadScene(leaderBoardScene);
    }
}
