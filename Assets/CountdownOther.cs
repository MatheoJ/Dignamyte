using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountdownOther : MonoBehaviour
{
    // Start is called before the first frame update
    
    public UnityEvent eventHandler;
        
    // Start is called before the first frame update
    
    IEnumerator CountDownCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        eventHandler?.Invoke();
    }
    
    public void StartCountDown(float time)
    {
        StartCoroutine(CountDownCoroutine(time));
    }
}
