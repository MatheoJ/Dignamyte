using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLogger
{
    private static bool _enabledLogging = true;

    public static void Log(string msg)
    {
        if (_enabledLogging)
        {
            Debug.Log(msg);
        }
    }
    
}
