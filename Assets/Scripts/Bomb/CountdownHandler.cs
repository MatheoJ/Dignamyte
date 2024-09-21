using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    public float delay;

    public UnityEvent eventHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCountDown(delay);
    }

    IEnumerator CountDownCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        eventHandler?.Invoke();
    }

    public void StartCountDown(float time)
    {
        StartCoroutine(CountDownCoroutine(time));
    }

    // Update is called once per frame
}
