using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using AssetBundles;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour {
    private Dropdown objectList;
    private List<string> LocalItemsKey = new List<string>();
    private Dictionary<string, localDB.ObjectModel> localItemsDictionary = new Dictionary<string, localDB.ObjectModel>();
    private GameObject current3DObject = null;
    private void Awake()
    {
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
    public static bool GetPrimitiveType(string name, out PrimitiveType type)
    {
        bool result = true;
        type = PrimitiveType.Capsule;
        switch (name)
        {
            case "Sphere":
                type = PrimitiveType.Sphere;
                break;
            case "Capsule":
                type = PrimitiveType.Capsule;
                break;
            case "Cylinder":
                type = PrimitiveType.Cylinder;
                break;
            case "Cube":
                type = PrimitiveType.Cube;
                break;
            case "Plane":
                type = PrimitiveType.Plane;
                break;
            case "Quad":
                type = PrimitiveType.Quad;
                break;
            default:
                result = false;
                break;
        }
        return result;
    }
    private IEnumerator OldLoadRemoteAssetBundle(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        DontDestroyOnLoad(gameObject);
        // This is simply to get the elapsed time for this phase of AssetLoading.
        float startTime = Time.realtimeSinceStartup;
        AssetBundleManager.SetSourceAssetBundleURL(Item.AssetBundleUri);
        var InitRequest = AssetBundleManager.Initialize();
        if (InitRequest != null)
            yield return StartCoroutine(InitRequest);
        var assetRequest = AssetBundleManager.LoadAssetAsync(Item.AssetBundleName, Item.Name, typeof(GameObject));
        if (assetRequest == null)
            yield break;
        yield return StartCoroutine(assetRequest);
        if (assetRequest != null)
        {

            // Get the asset.
            GameObject prefab = assetRequest.GetAsset<GameObject>();
            // code below could be used to position the AssetBundles
            //prefab.transform.position = new Vector3(0,1,4); 
            if (prefab != null)
            {

                GameObject obj = Instantiate(prefab) as GameObject;
                obj.transform.eulerAngles = new Vector3(Item.rotationX, Item.rotationY, Item.rotationZ);
                obj.transform.localScale = new Vector3(Item.scaleX, Item.scaleY, Item.scaleZ);
                if (parentGameObject != null)
                    obj.transform.parent = parentGameObject.transform;
                obj.transform.localPosition = new Vector3(Item.positionX, Item.positionY, Item.positionZ);

                SetCurrent3DObject(obj);
            }

            // Calculate and display the elapsed time.
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(Item.Name + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");
        }

    }
    public static GameObject GetRemoteAssetBundle(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        RemoveAssetBundle(Item.AssetBundleName);
        // This is simply to get the elapsed time for this phase of AssetLoading.
        float startTime = Time.realtimeSinceStartup;
        AssetBundleManager.SetSourceAssetBundleURL(Item.AssetBundleUri);
        var InitRequest = AssetBundleManager.Initialize();
        if (InitRequest != null)
            WaitCoroutine(InitRequest);
        var assetRequest = AssetBundleManager.LoadAssetAsync(Item.AssetBundleName, Item.Name, typeof(GameObject));
        if (assetRequest == null)
            return null;
        WaitCoroutine(assetRequest);
        GameObject obj = null;
        if (assetRequest != null)
        {

            // Get the asset.
            GameObject prefab = assetRequest.GetAsset<GameObject>();
            // code below could be used to position the AssetBundles
            //prefab.transform.position = new Vector3(0,1,4); 
            if (prefab != null)
            {

                obj = Instantiate(prefab) as GameObject;
                obj.transform.eulerAngles = new Vector3(Item.rotationX, Item.rotationY, Item.rotationZ);
                obj.transform.localScale = new Vector3(Item.scaleX, Item.scaleY, Item.scaleZ);
                if (parentGameObject != null)
                    obj.transform.parent = parentGameObject.transform;
                obj.transform.localPosition = new Vector3(Item.positionX, Item.positionY, Item.positionZ);
            }

            // Calculate and display the elapsed time.
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(Item.Name + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");
        }
        return obj;
    }
    public static void RemoveAssetBundle(string name)
    {
        foreach (AssetBundle a in AssetBundle.GetAllLoadedAssetBundles())
        {
            if (a.name == name)
            {
                a.Unload(true);
                break;
            }
        }
    }
    public static GameObject GetLocalAssetBundle(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        RemoveAssetBundle(Item.AssetBundleName);
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, Item.AssetBundleName));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle from " + Application.streamingAssetsPath + "!");
            return null;
        }
        GameObject obj = null;
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>(Item.Name);
        if (prefab != null)
        {

            obj = Instantiate(prefab) as GameObject;
            obj.transform.eulerAngles = new Vector3(Item.rotationX, Item.rotationY, Item.rotationZ);
            obj.transform.localScale = new Vector3(Item.scaleX, Item.scaleY, Item.scaleZ);
            if (parentGameObject != null)
                obj.transform.parent = parentGameObject.transform;
            obj.transform.localPosition = new Vector3(Item.positionX, Item.positionY, Item.positionZ);
        }

        return obj;
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
    private void LoadRemoteAssetBundle(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        GameObject obj = GetRemoteAssetBundle(Item, parentGameObject);
        if (obj != null)
        {
            obj.transform.parent = gameObject.transform;
            SetCurrent3DObject(obj);

        }
    }
    public static void WaitCoroutine(IEnumerator func)
    {
        while (func.MoveNext())
        {
            if (func.Current != null)
            {
                IEnumerator num;
                try
                {
                    num = (IEnumerator)func.Current;
                }
                catch (System.InvalidCastException)
                {
                    if (func.Current.GetType() == typeof(WaitForSeconds))
                        Debug.LogWarning("Skipped call to WaitForSeconds. Use WaitForSecondsRealtime instead.");
                    return;  // Skip WaitForSeconds, WaitForEndOfFrame and WaitForFixedUpdate
                }
                WaitCoroutine(num);
            }
        }
    }
    public static GameObject  GetLocalPrefab(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        Object res = Resources.Load(Item.Name, typeof(GameObject));
        if (res != null)
        {
            GameObject obj = Instantiate(res) as GameObject;
            obj.transform.eulerAngles = new Vector3(Item.rotationX, Item.rotationY, Item.rotationZ);
            obj.transform.localScale = new Vector3(Item.scaleX, Item.scaleY, Item.scaleZ);
            if (parentGameObject != null)
                obj.transform.parent = parentGameObject.transform;
            obj.transform.localPosition = new Vector3(Item.positionX, Item.positionY, Item.positionZ);
            return obj;
        }
        return null;
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
    public static GameObject GetLocalPrimitive(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        PrimitiveType pt = PrimitiveType.Capsule;

        if (GetPrimitiveType(Item.Name, out pt) == true)
        {
            GameObject obj = GameObject.CreatePrimitive(pt);
            obj.transform.eulerAngles = new Vector3(Item.rotationX, Item.rotationY, Item.rotationZ);
            obj.transform.localScale = new Vector3(Item.scaleX, Item.scaleY, Item.scaleZ);
            if(parentGameObject!=null)
            obj.transform.parent = parentGameObject.transform;
            obj.transform.localPosition = new Vector3(Item.positionX, Item.positionY, Item.positionZ);

            return obj;
        }
        return null;
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
