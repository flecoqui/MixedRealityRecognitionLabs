using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Vuforia;

public class VuforiaManager : MonoBehaviour {
    public static VuforiaManager instance = null;
    private static bool bInit = false;
    public string[] sceneNameWithAR = { "RecognitionScene" };
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
        if (bInit == false)
        {
            bInit = true;
            VuforiaRuntime.Instance.InitVuforia();
        }
       

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }


    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(!sceneNameWithAR.Contains(arg0.name))
        {
            
            VuforiaBehaviour.Instance.enabled = false;
        }
        else
        {
            VuforiaBehaviour.Instance.enabled = true;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

}
