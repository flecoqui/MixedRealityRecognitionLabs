using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyARCamera : MonoBehaviour {
    public static DontDestroyARCamera instance = null;
    // Use this for initialization

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }


}
