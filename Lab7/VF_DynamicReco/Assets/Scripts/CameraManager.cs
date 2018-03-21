using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Vuforia;
using System.Reflection;
using System;
/// <summary>

/// Virtual Camera. Switches between a main camera and an ar camera

/// </summary>

public class CameraManager : MonoBehaviour
{

    public enum eCameraType { mCamera, vCamera }





    private Camera mCamera;



    private Camera vCamera;



    private VuforiaBehaviour vuforiaBehaviour;



    private VuforiaARController varc;

    public eCameraType cameraType { get; private set; }



    private Camera cCamera;

    private AudioListener asMCamera;

    private AudioListener asVCamera;



    private System.Action onCameraReady;


    public static CameraManager Instance = null;
    // State before start:

    // mCamera is a typical camera without any Vuforia Behaviour. Audio listener enabled

    // vCamera is a Vuforia ARCamera with VuforiaBehaviour disabled. Audio Listener disabled

    private void Awake()
    {
        Instance = this;

        foreach (Camera c in Camera.allCameras)
        {
            if (c.gameObject.name == "Main Camera")
                this.mCamera = c;
            else if (c.gameObject.name == "ARCamera")
                this.vCamera = c;
            if ((this.vCamera != null) && (this.mCamera != null))
                break;
        }
        if ((this.vCamera == null) && (this.mCamera == null))
        {
            Debug.LogError("Camera are missing");
            return;
        }
        this.varc = VuforiaARController.Instance;



        this.cCamera = this.mCamera;

        this.cameraType = eCameraType.mCamera;



        this.asMCamera = this.mCamera.GetComponent<AudioListener>();
        if (this.vCamera != null)
        {
            this.asVCamera = this.vCamera.GetComponent<AudioListener>();



            this.vuforiaBehaviour = this.vCamera.GetComponent<VuforiaBehaviour>();
        }

    }
    private void ReadyFunc()
    {
        if (this.cameraType == eCameraType.mCamera)
            Debug.Log("Main Camera is ready");
        else
            Debug.Log("AR Camera is ready");
    }
    public void Start()
    {
        setCamera(ReadyFunc, eCameraType.mCamera);
    }

    private void onVuforiaStarted()
    {

        this.varc.UnregisterVuforiaStartedCallback(this.onVuforiaStarted);

        this.onCameraReady();

    }



    /// <summary>

    /// Sets Main Camera or AR Camera. onCameraReady is called when finished

    /// </summary>

    /// <param name="onCameraReady"></param>

    /// <param name="cameraType"></param>

    public void setCamera(System.Action onCameraReady, eCameraType cameraType)
    {

        this.onCameraReady = onCameraReady;



        // if (this.cameraType != cameraType)
        {

            this.cameraType = cameraType;



            if (this.cameraType == eCameraType.mCamera)
            {
                if (this.asVCamera != null)
                    this.asVCamera.enabled = false;

                this.asMCamera.enabled = true;



                this.cCamera = this.mCamera;
                if (this.vuforiaBehaviour != null)
                    this.vuforiaBehaviour.enabled = false;



                this.onCameraReady();

            }
            else
            {

                this.varc.RegisterVuforiaStartedCallback(this.onVuforiaStarted);



                this.asMCamera.enabled = false;
                if (this.asVCamera != null)
                    this.asVCamera.enabled = true;



                this.cCamera = this.vCamera;

                if (this.vuforiaBehaviour != null)
                    this.vuforiaBehaviour.enabled = true;

            }

        }
        //else
        //{

        //    this.onCameraReady();

        //}

    }

}


