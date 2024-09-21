using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private Text killCountText;
    
    [SerializeField]
    private Text timeText;
    
    private float timeSinceGameStart = 0.0f;
    
    public void UpdateKillCount(int killCount)
    {
        killCountText.text = "Kills: " + killCount;
    }
    
    public void UpdateTimeSinceStart(float time)
    {
        // Convert the time to minutes and seconds
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    
    public void UpdateBombCount(int currentBombCount, int maxBombCount)
    {
        // Update the bomb count text
        timeText.text = "Bombs: " + currentBombCount + "/" + maxBombCount;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timeSinceGameStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeSinceStart(Time.time - timeSinceGameStart);
    }
}
