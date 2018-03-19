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
        }

        if ((StateManager.instance != null) &&(StateManager.instance.currentModel != null))
        {
           // var objectTracker = TrackerManager.Instance.GetTracker<MultiTarget>();
            var target = GameObject.Find("MultiTarget");
            if (target != null)
            {
                var Item = StateManager.instance.currentModel;
                if (Item != null)
                {
                    GameObject obj = null;
                    if (Item.Type == localDB.ObjectModel.TypePrimitive)
                    {
                        obj = MainSceneUI.GetLocalPrimitive(Item,target);
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
                        obj = MainSceneUI.GetLocalAssetBundle(Item, target);
                    }
                    if(obj!=null)
                    {
                       // obj.transform.parent = target.transform;
                        current3DObject = obj;
                    }
                }
            }
        }


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
