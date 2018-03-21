using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Vuforia;
using System.Reflection;
using System;

public class VuforiaCameraIssueFix : MonoBehaviour
{
    void Awake()
    {
        try
        {
            EventInfo evSceneLoaded = typeof(SceneManager).GetEvent("sceneLoaded");
            Type tDelegate = evSceneLoaded.EventHandlerType;

            MethodInfo attachHandler = typeof(VuforiaRuntime).GetMethod("AttachVuforiaToMainCamera", BindingFlags.NonPublic | BindingFlags.Static);
#if UNITY_EDITOR
            Delegate d = Delegate.CreateDelegate(tDelegate, attachHandler);
#else
            Delegate d = attachHandler.CreateDelegate(tDelegate);
#endif
            SceneManager.sceneLoaded -= d as UnityEngine.Events.UnityAction<Scene, LoadSceneMode>;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant remove the AttachVuforiaToMainCamera action: " + e.ToString());
        }
    }
}