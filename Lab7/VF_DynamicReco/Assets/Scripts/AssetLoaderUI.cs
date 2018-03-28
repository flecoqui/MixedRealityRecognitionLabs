using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;
using System.IO;
using UnityEngine.SceneManagement;
public class AssetLoaderUI : MonoBehaviour {

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
    public IEnumerator GetRemoteAssetBundleAsync(localDB.ObjectModel Item, GameObject parentGameObject, System.Action<GameObject> AssetLoadedCallBack)
    {
        Debug.Log("Loading AssetBundle: " + Item.AssetBundleName + " AssetName: " + Item.Name);
        RemoveAssetBundle(Item.AssetBundleName);
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
        GameObject obj = null;
        if (assetRequest != null)
        {
            Debug.Log("AssetBundle loaded: " + Item.AssetBundleName );

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
        else
            Debug.Log("AssetBundle not loaded: " + Item.AssetBundleName );

        if (AssetLoadedCallBack != null)
            AssetLoadedCallBack(obj);

    }


    public  void RemoveAssetBundle(string name)
    {
        AssetBundle.UnloadAllAssetBundles(true);


        //foreach (AssetBundle a in AssetBundle.GetAllLoadedAssetBundles())
        //{
        //    if (a.name == name)
        //    {
        //        a.Unload(true);
        //        break;
        //    }
        //}
        AssetBundleManager.UnloadAllAssetBundles();
    }
    public  GameObject GetLocalAssetBundle(localDB.ObjectModel Item, GameObject parentGameObject)
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




    public  GameObject GetLocalPrefab(localDB.ObjectModel Item, GameObject parentGameObject)
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

    public  GameObject GetLocalPrimitive(localDB.ObjectModel Item, GameObject parentGameObject)
    {
        PrimitiveType pt = PrimitiveType.Capsule;

        if (GetPrimitiveType(Item.Name, out pt) == true)
        {
            GameObject obj = GameObject.CreatePrimitive(pt);
            obj.transform.eulerAngles = new Vector3(Item.rotationX, Item.rotationY, Item.rotationZ);
            obj.transform.localScale = new Vector3(Item.scaleX, Item.scaleY, Item.scaleZ);
            if (parentGameObject != null)
                obj.transform.parent = parentGameObject.transform;
            obj.transform.localPosition = new Vector3(Item.positionX, Item.positionY, Item.positionZ);

            return obj;
        }
        return null;
    }



}

