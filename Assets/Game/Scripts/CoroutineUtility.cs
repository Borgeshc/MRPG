using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtility : MonoBehaviour
{
    #region Instance
    //Version of instance taken from "http://wiki.unity3d.com/index.php/AManagerClass"
    private static CoroutineUtility s_Instance = null;
    public static CoroutineUtility instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(CoroutineUtility)) as CoroutineUtility;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("CoroutineUtility");
                s_Instance = obj.AddComponent(typeof(CoroutineUtility)) as CoroutineUtility;
                Debug.Log("Could not locate an CoroutineUtility object. CoroutineUtility was Generated Automaticly.");
            }

            return s_Instance;
        }
    }
    #endregion
}
