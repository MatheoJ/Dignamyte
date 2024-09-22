using UnityEngine;
using UnityEngine.Events;

public class WaitForInput : MonoBehaviour
{
    public UnityEvent eventHandler;
     
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            eventHandler?.Invoke();
            enabled = false;
        }
    }
}
