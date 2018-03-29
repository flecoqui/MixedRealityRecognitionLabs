using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using AssetBundles;
using UnityEngine.SceneManagement;
using Vuforia;

public class RecognitionSceneUI : AssetLoaderUI {


    private GameObject current3DObject = null;
    public static RecognitionSceneUI Instance = null;
    public void AssetLoaded(GameObject obj)
    {
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
    public void ShowSaveButton(bool bShow)
    {

        var CanvasObject = GameObject.Find("SaveCanvas").GetComponent<Canvas>();
        CanvasObject.GetComponent<Canvas>().enabled = bShow;

    }
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
            ShowSaveButton(false);
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
                        obj = GetLocalPrimitive(Item, target);
                        AssetLoaded(obj);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeLocalPrefab)
                    {
                        obj = GetLocalPrefab(Item, target);
                        AssetLoaded(obj);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeLocalAssetBundle)
                    {
                        obj = GetLocalAssetBundle(Item, target);
                        AssetLoaded(obj);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeRemoteAssetBundle)
                    {
                        StartCoroutine(GetRemoteAssetBundleAsync(Item, target, AssetLoaded));
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
