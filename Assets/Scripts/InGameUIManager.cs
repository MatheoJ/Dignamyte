using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Text killCountText;
    
    public Text timeText;

    public Text powerUpText;
    
    public Image freezeClock;
    
    public Image invincibilityClock;
    
    public Image invisibilityIcon;
    
    public Image freezeIcon;
    
    private float timeSinceGameStart = 0.0f;
    
    public AudioSource powerUpSound;
    
    public AudioSource noBombSound;
    
    public void UpdateKillCount(int killCount)
    {
        killCountText.text = "Kills: " + killCount;
    }
    
    public void UpdateTimeSinceStart(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    
    void Start()
    {
        timeSinceGameStart = Time.time;
        
        // set icon alpha to 0.25
        Color iconColor = invisibilityIcon.color;
        iconColor.a = 0.25f;
        invisibilityIcon.color = iconColor;
        
        iconColor = freezeIcon.color;
        iconColor.a = 0.25f;
        freezeIcon.color = iconColor;
    }

    void Update()
    {
        UpdateTimeSinceStart(Time.time - timeSinceGameStart);
    }
    
    public void startFreezeClock(float duration)
    {
        freezeClock.fillAmount = 1.0f;
        StartCoroutine(UpdateFreezeClock(duration));
    }
    
    IEnumerator UpdateFreezeClock(float duration)
    {
        float timePassed = 0.0f;
        freezeClock.fillAmount = 1.0f;
        freezeClock.gameObject.SetActive(true);
        
        //Set alpha of icon to 1    
        Color iconColor = freezeIcon.color;
        iconColor.a = 1.0f;
        freezeIcon.color = iconColor;
        
        while (timePassed < duration)
        {
            freezeClock.fillAmount = 1.0f - timePassed / duration;
            timePassed += Time.deltaTime;
            yield return null;
        }
        
        //Set alpha of icon to 0.25
        iconColor.a = 0.25f;
        freezeIcon.color = iconColor;
        
        freezeClock.gameObject.SetActive(false);
        
    }
    
    public void startInvincibilityClock(float duration)
    {
        invincibilityClock.fillAmount = 1.0f;
        StartCoroutine(UpdateInvincibilityClock(duration));
    }
    
    IEnumerator UpdateInvincibilityClock(float duration)
    {
        float timePassed = 0.0f;
        
        invincibilityClock.gameObject.SetActive(true);
        invincibilityClock.fillAmount = 1.0f;
        
        //Set alpha of icon to 1
        Color iconColor = invisibilityIcon.color;
        iconColor.a = 1.0f;
        invisibilityIcon.color = iconColor;
        
        while (timePassed < duration)
        {
            invincibilityClock.fillAmount = 1.0f - timePassed / duration;
            timePassed += Time.deltaTime;
            yield return null;
        }
        
        //Set alpha of icon to 0.25
        iconColor.a = 0.25f;
        invisibilityIcon.color = iconColor;
        
        invincibilityClock.gameObject.SetActive(false);
        
    }
    
    public void displayPowerUpTextForDuration(string powerUpName, float duration)
    {
        powerUpText.text = powerUpName;
        powerUpText.gameObject.SetActive(true);
        StartCoroutine(FadeInAndOutPowerUpText(duration));
    }
    
    // Coroutine to handle fading in and out of the power-up text
    IEnumerator FadeInAndOutPowerUpText(float duration)
    {
        // Fade in
        yield return StartCoroutine(FadeInText());

        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Fade out
        yield return StartCoroutine(FadeOutText());
        
        powerUpText.gameObject.SetActive(false);
    }

    // Coroutine for fading in the text
    IEnumerator FadeInText()
    {
        Color textColor = powerUpText.color;
        textColor.a = 0;
        powerUpText.color = textColor;

        while (powerUpText.color.a < 1.0f)
        {
            textColor.a += Time.deltaTime / 0.7f; // Adjust this for fade-in speed
            powerUpText.color = textColor;
            yield return null;
        }
    }

    // Coroutine for fading out the text
    IEnumerator FadeOutText()
    {
        Color textColor = powerUpText.color;
        while (powerUpText.color.a > 0.0f)
        {
            textColor.a -= Time.deltaTime / 0.7f; // Adjust this for fade-out speed
            powerUpText.color = textColor;
            yield return null;
        }
    }

    public void displaySpeedBoostText()
    {
        displayPowerUpTextForDuration("Speed Boost", 1.0f);
    }
    
    public void displayRangeBoostText()
    {
        displayPowerUpTextForDuration("Bomb Range Boost", 1.0f);
    }
    
    public void displayBombNumberBoostText()
    {
        displayPowerUpTextForDuration("Bomb number Boost", 1.0f);
    }
    
    public void displayFreezeText()
    {
        displayPowerUpTextForDuration("Enemies are frozen", 1.0f);
    }
    
    public void displayInvincibilityText()
    {
        displayPowerUpTextForDuration("Invincibility Boost", 1.0f);
    }
}
