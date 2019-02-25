using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputLogOnScreen : MonoBehaviour
{
    string myLog;
    Queue myLogQueue = new Queue();


    void OnEnable ()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable ()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog ( string logString, string stackTrace, LogType type )
    {
        myLog = logString;
        string newString = "\n " + myLog;
        myLogQueue.Enqueue (newString);
        if ( type == LogType.Exception )
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue (newString);
        }
        myLog = string.Empty;
        foreach ( string mylog in myLogQueue )
        {
            myLog += mylog;
        }
        //if ( myLogQueue.Count > 6)
            myLogQueue.Clear ();
    }

    void OnGUI ()
    {
        GUI.contentColor = Color.black;
        GUILayout.Label (myLog);
    }
}
