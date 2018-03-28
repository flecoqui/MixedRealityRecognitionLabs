using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using AssetBundles;
using UnityEngine.SceneManagement;

public class MainSceneUI : AssetLoaderUI {
    private Dropdown objectList;
    private List<string> LocalItemsKey = new List<string>();
    private Dictionary<string, localDB.ObjectModel> localItemsDictionary = new Dictionary<string, localDB.ObjectModel>();
    private GameObject current3DObject = null;
    public static MainSceneUI Instance;
    private void Awake()
    {
        Instance = this;
        this.objectList = GetComponentInChildren<Dropdown>();
        if (this.objectList == null)
        {
            Debug.LogError("There wasn't a DropDown in the GameObject hierarchy.\n");
            return;
        }
        // populate the list
        int index = -1;
        int count = 0;
        this.objectList.ClearOptions();
        List<localDB.ObjectModel> list = localDB.GetObjectList();
        foreach (var o in list)
        {
            localItemsDictionary.Add(o.ID, o);
            LocalItemsKey.Add(o.ID);
            if ((!string.IsNullOrEmpty(StateManager.instance.ModelId)) && (string.Equals(StateManager.instance.ModelId, o.ID)))
                index = count;
            count++;
        }

        this.objectList.AddOptions(this.LocalItemsKey);
        if (!string.IsNullOrEmpty(StateManager.instance.ModelId))
        {
            this.objectList.value = index;
        }
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
    }
    void SetCurrent3DObject(GameObject obj)
    {
        if (obj != null)
        {
            if (current3DObject != null)
            {
                Destroy(current3DObject);
                current3DObject = null;
            }
            current3DObject = obj;
        }
    }






    private void LoadLocalAssetBundle(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        GameObject obj = GetLocalAssetBundle(Item, parentGameObject);
        if (obj != null)
        {
            obj.transform.parent = gameObject.transform;
            SetCurrent3DObject(obj);

        }
    }
    public void AssetLoaded(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.parent = gameObject.transform;
            SetCurrent3DObject(obj);

        }
    }
    private void LoadRemoteAssetBundle(localDB.ObjectModel Item, GameObject parentGameObject)
    {

        MainSceneUI.Instance.StartCoroutine(MainSceneUI.Instance.GetRemoteAssetBundleAsync(Item, parentGameObject, AssetLoaded));
    }



    private void LoadLocalPrefab(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        GameObject obj = GetLocalPrefab(Item, parentGameObject);
        if (obj != null)
        {
            obj.transform.parent = gameObject.transform;
            SetCurrent3DObject(obj);
        }
    }

    private void LoadLocalPrimitive(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        GameObject obj = GetLocalPrimitive(Item, parentGameObject);
        if (obj != null)
        {
            
            SetCurrent3DObject(obj);
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
            case "View":
                var selectedItem = objectList.options[objectList.value].text;
                var Item = localItemsDictionary[selectedItem];
                if (Item != null)
                {
                    var target = GameObject.Find("TargetText");
                    if (target != null)
                    { 
                        if (Item.Type == localDB.ObjectModel.TypePrimitive)
                        {
                            LoadLocalPrimitive(Item, target);
                        }
                        else if (Item.Type == localDB.ObjectModel.TypeLocalPrefab)
                        {
                            LoadLocalPrefab(Item, target);
                        }
                        else if (Item.Type == localDB.ObjectModel.TypeLocalAssetBundle)
                        {
                            LoadLocalAssetBundle(Item, target);
                        }
                        else if (Item.Type == localDB.ObjectModel.TypeRemoteAssetBundle)
                        {
                            LoadRemoteAssetBundle(Item, target);
                        //    StartCoroutine(OldLoadRemoteAssetBundle(Item, target));
                        }
                    }
                }
                break;
            case "Open":
                StateManager.instance.ModelId = objectList.options[objectList.value].text;
                StateManager.instance.currentModel = localItemsDictionary[StateManager.instance.ModelId];
                SceneManager.LoadScene("RecognitionScene");
                break;
            default:
                Debug.LogWarningFormat("Button '{0}' not handled.", label.text);
                break;
        }
    }

}
