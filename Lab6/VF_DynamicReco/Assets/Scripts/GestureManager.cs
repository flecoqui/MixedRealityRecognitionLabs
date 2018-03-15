using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;


public class GestureManager : MonoBehaviour {
    public static GestureManager instance;
    GestureRecognizer recognizer;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += TapHandler;
        recognizer.StartCapturingGestures();
    }
    private void TapHandler(TappedEventArgs obj)
    {
        if(Input.GetButton("PannelButton"))
        {
            Debug.Log("Button pressed");
        }
        else
        {
            Debug.Log("Button not pressed");
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
    void OnDestroy()
    {
        recognizer.Tapped -= TapHandler;

    }
}
