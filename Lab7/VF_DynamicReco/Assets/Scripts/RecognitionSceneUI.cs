using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using AssetBundles;
using UnityEngine.SceneManagement;
using Vuforia;

public class RecognitionSceneUI : MonoBehaviour {


    private GameObject current3DObject = null;
    public static RecognitionSceneUI Instance = null;
    private void Awake()
    {
        Instance = this;
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
            if ((label.text == "Save") || (label.text == "Do Nothing"))
                label.text = "Do Nothing";

            button.onClick.AddListener(() => Button_OnClick(label));
        }

        if ((StateManager.instance != null) && (StateManager.instance.currentModel != null))
        {
            var target = GameObject.Find("MultiTarget");
            if (target != null)
            {
                var Item = StateManager.instance.currentModel;
                if (Item != null)
                {
                    GameObject obj = null;
                    if (Item.Type == localDB.ObjectModel.TypePrimitive)
                    {
                        obj = MainSceneUI.GetLocalPrimitive(Item, target);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeLocalPrefab)
                    {
                        obj = MainSceneUI.GetLocalPrefab(Item, target);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeLocalAssetBundle)
                    {
                        obj = MainSceneUI.GetLocalAssetBundle(Item, target);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeRemoteAssetBundle)
                    {
                        obj = MainSceneUI.GetRemoteAssetBundle(Item, target);
                    }
                    if (obj != null)
                    {
                        current3DObject = obj;
                        List<Renderer> list = new List<Renderer>();
                        current3DObject.GetComponents<Renderer>(list);
                        if (list.Count == 0)
                        {
                            current3DObject.GetComponentsInChildren<Renderer>(list);
                        }
                        if (list.Count > 0)
                        {
                            foreach (var r in list)
                            {
                                r.enabled = false;
                            }
                        }

                    }
                }
            }
        }


    }
    private void Start()
    {

        CameraManager.Instance.setCamera(onCameraReady, CameraManager.eCameraType.vCamera);
        
    }
    void OnDestroy()
    {
        if (current3DObject != null)
        {
            Destroy(current3DObject);
            current3DObject = null;
        }
    }
    private void onCameraReady()
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
            case "Save":
                {

                    if (current3DObject != null)
                    {
                        StateManager.instance.ModelTargetPosition = current3DObject.transform.position;
                        StateManager.instance.ModelTargetAngles = current3DObject.transform.eulerAngles;
                        StateManager.instance.ModelTargetScale = current3DObject.transform.localScale;
                        SceneManager.LoadScene("PositionScene");
                    }
                }
                break;
            case "Back":
                SceneManager.LoadScene("MainScene");
                break;

            default:
                Debug.LogWarningFormat("Button '{0}' not handled.", label.text);
                break;
        }
    }
}
