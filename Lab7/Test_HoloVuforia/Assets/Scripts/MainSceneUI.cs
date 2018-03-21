using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class MainSceneUI : MonoBehaviour {
    private void Awake()
    {
        var buttonList = GetComponentsInChildren<Button>();
        if (buttonList.Length == 0)
        {
            Debug.LogError("There are no buttons in the GameObject hierarchy.\n");
            Debug.LogError("Place this script on the root of the UI.");

            return;
        }

        foreach (var button in buttonList)
        {
            var label = button.GetComponentInChildren<Text>();
            
            button.onClick.AddListener(() => Button_OnClick(label));

            var edit = button.GetComponentInChildren<Text>();
            if ((string.Equals(edit.text, "Camera")) || (string.Equals(edit.text, "ARCamera")))
            {
                if ((CameraManager.Instance!=null) &&(CameraManager.Instance.cameraType == CameraManager.eCameraType.vCamera))
                    edit.text = "Camera";
                else
                    edit.text = "ARCamera";
            }
        }
    }
    private void ReadyFunc()
    {
        if (CameraManager.Instance.cameraType == CameraManager.eCameraType.mCamera)
            Debug.Log("Main Camera is ready");
        else
            Debug.Log("AR Camera is ready");
    }
    private void Button_OnClick(Text label)
    {
        if (label == null)
        {
            return;
        }

        switch (label.text)
        {
            case "ARCamera":
                {
                    CameraManager.Instance.setCamera(ReadyFunc, CameraManager.eCameraType.vCamera);
                    var buttonList = GetComponentsInChildren<Button>();
                    foreach (var button in buttonList)
                    {
                        var edit = button.GetComponentInChildren<Text>();
                        if ((string.Equals(edit.text, "Camera")) || (string.Equals(edit.text, "ARCamera")))
                        {
                            if (CameraManager.Instance.cameraType == CameraManager.eCameraType.mCamera)
                                edit.text = "ARCamera";
                            else
                                edit.text = "Camera";
                        }
                    }
                }
                break;
            case "Camera":
                {
                    CameraManager.Instance.setCamera(ReadyFunc, CameraManager.eCameraType.mCamera);
                    var buttonList = GetComponentsInChildren<Button>();
                    foreach (var button in buttonList)
                    {
                        var edit = button.GetComponentInChildren<Text>();
                        if ((string.Equals(edit.text, "Camera")) || (string.Equals(edit.text, "ARCamera")))
                        {

                            if (CameraManager.Instance.cameraType == CameraManager.eCameraType.mCamera)
                                edit.text = "ARCamera";
                            else
                                edit.text = "Camera";
                        }
                    }
                }
                break;
            case "Back":
                SceneManager.LoadScene("FirstScene");
                break;
            default:
                Debug.LogWarningFormat("Button '{0}' not handled.", label.text);
                break;
        }
    }
}
