﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using AssetBundles;
using UnityEngine.SceneManagement;
public class PositionSceneUI : MonoBehaviour {
    private GameObject current3DObject = null;
    private bool bShow = true;
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


        if ((StateManager.instance != null) && (StateManager.instance.currentModel != null))
        {

                var Item = StateManager.instance.currentModel;
                if (Item != null)
                {
                    GameObject obj = null;
                    if (Item.Type == localDB.ObjectModel.TypePrimitive)
                    {
                        obj = MainSceneUI.GetLocalPrimitive(Item, null);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeLocalPrefab)
                    {
                        obj = MainSceneUI.GetLocalPrefab(Item, null);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeLocalAssetBundle)
                    {
                        obj = MainSceneUI.GetLocalAssetBundle(Item, null);
                    }
                    else if (Item.Type == localDB.ObjectModel.TypeRemoteAssetBundle)
                    {
                        obj = MainSceneUI.GetLocalAssetBundle(Item, null);
                    }
                    if (obj != null)
                    {
                        obj.transform.position = StateManager.instance.ModelTargetPosition;
                        current3DObject = obj;
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
            case "Hide":
            case "Show":
                {
                    current3DObject.GetComponent<Renderer>().enabled = !current3DObject.GetComponent<Renderer>().enabled;
                    var buttonList = GetComponentsInChildren<Button>();
                    foreach (var button in buttonList)
                    {
                        var edit = button.GetComponentInChildren<Text>();
                        if ((string.Equals(edit.text, "Hide"))|| (string.Equals(edit.text, "Show")))
                        {
                            if (current3DObject.GetComponent<Renderer>().enabled == true)
                                edit.text = "Hide";
                            else
                                edit.text = "Show";
                        }
                    }
                }
                break;
            case "Back":
                SceneManager.LoadScene("RecognitionScene");
                break;
            default:
                Debug.LogWarningFormat("Button '{0}' not handled.", label.text);
                break;
        }
    }
}
