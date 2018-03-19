using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class localDB : MonoBehaviour {
    public class ObjectModel {
        public const string TypePrimitive = "Primitive";
        public const string TypeLocalPrefab = "Prefab";
        public const string TypeLocalAssetBundle = "LocalAssetBundle";
        public const string TypeRemoteAssetBundle = "RemoteAssetBundle";
        public string ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string AssetBundleName { get; set; }
        public string AssetBundleUri { get; set; }
        public float positionX { get; set; }
        public float positionY { get; set; }
        public float positionZ { get; set; }
        public float rotationX { get; set; }
        public float rotationY { get; set; }
        public float rotationZ { get; set; }
        public float scaleX { get; set; }
        public float scaleY { get; set; }
        public float scaleZ { get; set; }
    }

    static List<ObjectModel> objectModelItems = new List<ObjectModel> {
    // Primitive objects
    //new ObjectModel { ID = "CubeID",  Type = ObjectModel.TypePrimitive,Name = "Cube", AssetBundleName = "", AssetBundleUri = "", positionX = -1f, positionY = 0f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel  { ID = "SphereID", Type = ObjectModel.TypePrimitive, Name = "Sphere",AssetBundleName = "", AssetBundleUri = "",  positionX = 0f, positionY = 0f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel { ID = "CylinderID",  Type = ObjectModel.TypePrimitive, Name = "Cylinder", AssetBundleName = "", AssetBundleUri = "", positionX = 1f, positionY = 0f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //// Prefabs objects in resources folder
    //new ObjectModel { ID = "CubeIDInResource",  Type = ObjectModel.TypeLocalPrefab,Name = "MyCube", AssetBundleName = "", AssetBundleUri = "", positionX = -1f, positionY = -0.3f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel  { ID = "SphereIDInResource", Type = ObjectModel.TypeLocalPrefab, Name = "MySphere", AssetBundleName = "", AssetBundleUri = "", positionX = 0f, positionY = -0.3f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel { ID = "CylinderIDInResource",  Type = ObjectModel.TypeLocalPrefab, Name = "MyCylinder", AssetBundleName = "", AssetBundleUri = "", positionX = 1f, positionY = -0.3f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel { ID = "SkeletonIDInResource",  Type = ObjectModel.TypeLocalPrefab, Name = "MySkeleton",AssetBundleName = "", AssetBundleUri = "",  positionX = 1f, positionY = -0.3f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 3f, scaleY = 3f, scaleZ = 3f },
    //// local AssetBundle
    //new ObjectModel { ID = "CubeIDInLocalAssetBundle",  Type = ObjectModel.TypeLocalAssetBundle,Name = "MyCube", AssetBundleName = "mycube",  AssetBundleUri = "",positionX = -1f, positionY = -0.6f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel  { ID = "SphereIDInLocalAssetBundle", Type = ObjectModel.TypeLocalAssetBundle, Name = "MySphere",AssetBundleName = "mysphere",  AssetBundleUri = "", positionX = 0f, positionY = -0.6f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel { ID = "CylinderIDInLocalAssetBundle",  Type = ObjectModel.TypeLocalAssetBundle, Name = "MyCylinder",AssetBundleName = "mycylinder",  AssetBundleUri = "", positionX = 1f, positionY = -0.6f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel { ID = "SkeletonIDInLocalAssetBundle",  Type = ObjectModel.TypeLocalAssetBundle, Name = "MySkeleton", AssetBundleName = "myskeleton", AssetBundleUri = "", positionX = 1f, positionY = -0.6f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 3f, scaleY = 3f, scaleZ = 3f },
    //// remote AssetBundle
    //new ObjectModel { ID = "CubeIDInRemoteAssetBundle",  Type = ObjectModel.TypeRemoteAssetBundle,Name = "MyCube", AssetBundleName = "mycube", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/", positionX = -1f, positionY = -0.9f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel  { ID = "SphereIDInRemoteAssetBundle", Type = ObjectModel.TypeRemoteAssetBundle, Name = "MySphere",AssetBundleName = "mysphere", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/",  positionX = 0f, positionY = -0.9f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel { ID = "CylinderIDInRemoteAssetBundle",  Type = ObjectModel.TypeRemoteAssetBundle, Name = "MyCylinder",AssetBundleName = "mycylinder", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/",  positionX = 1f, positionY = -0.9f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    //new ObjectModel { ID = "SkeletonIDInRemoteAssetBundle",  Type = ObjectModel.TypeRemoteAssetBundle, Name = "MySkeleton", AssetBundleName = "myskeleton", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/", positionX = 1f, positionY = -0.9f, positionZ = 3f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 3f, scaleY = 3f, scaleZ = 3f }
    // Primitive objects
    new ObjectModel { ID = "CubeID",  Type = ObjectModel.TypePrimitive,Name = "Cube", AssetBundleName = "", AssetBundleUri = "", positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel  { ID = "SphereID", Type = ObjectModel.TypePrimitive, Name = "Sphere",AssetBundleName = "", AssetBundleUri = "",  positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel { ID = "CylinderID",  Type = ObjectModel.TypePrimitive, Name = "Cylinder", AssetBundleName = "", AssetBundleUri = "", positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    // Prefabs objects in resources folder
    new ObjectModel { ID = "CubeIDInResource",  Type = ObjectModel.TypeLocalPrefab,Name = "MyCube", AssetBundleName = "", AssetBundleUri = "", positionX = 0f, positionY = -0.3f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel  { ID = "SphereIDInResource", Type = ObjectModel.TypeLocalPrefab, Name = "MySphere", AssetBundleName = "", AssetBundleUri = "", positionX = 0f, positionY = -0.3f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel { ID = "CylinderIDInResource",  Type = ObjectModel.TypeLocalPrefab, Name = "MyCylinder", AssetBundleName = "", AssetBundleUri = "", positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel { ID = "SkeletonIDInResource",  Type = ObjectModel.TypeLocalPrefab, Name = "MySkeleton",AssetBundleName = "", AssetBundleUri = "",  positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 3f, scaleY = 3f, scaleZ = 3f },
    // local AssetBundle
    new ObjectModel { ID = "CubeIDInLocalAssetBundle",  Type = ObjectModel.TypeLocalAssetBundle,Name = "MyCube", AssetBundleName = "mycube",  AssetBundleUri = "",positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel  { ID = "SphereIDInLocalAssetBundle", Type = ObjectModel.TypeLocalAssetBundle, Name = "MySphere",AssetBundleName = "mysphere",  AssetBundleUri = "", positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel { ID = "CylinderIDInLocalAssetBundle",  Type = ObjectModel.TypeLocalAssetBundle, Name = "MyCylinder",AssetBundleName = "mycylinder",  AssetBundleUri = "", positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel { ID = "SkeletonIDInLocalAssetBundle",  Type = ObjectModel.TypeLocalAssetBundle, Name = "MySkeleton", AssetBundleName = "myskeleton", AssetBundleUri = "", positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 3f, scaleY = 3f, scaleZ = 3f },
    // remote AssetBundle
    new ObjectModel { ID = "CubeIDInRemoteAssetBundle",  Type = ObjectModel.TypeRemoteAssetBundle,Name = "MyCube", AssetBundleName = "mycube", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/", positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 45f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel  { ID = "SphereIDInRemoteAssetBundle", Type = ObjectModel.TypeRemoteAssetBundle, Name = "MySphere",AssetBundleName = "mysphere", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/",  positionX = 0f, positionY = -0.9f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel { ID = "CylinderIDInRemoteAssetBundle",  Type = ObjectModel.TypeRemoteAssetBundle, Name = "MyCylinder",AssetBundleName = "mycylinder", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/",  positionX = 0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 0.2f, scaleY = 0.2f, scaleZ = 0.2f },
    new ObjectModel { ID = "SkeletonIDInRemoteAssetBundle",  Type = ObjectModel.TypeRemoteAssetBundle, Name = "MySkeleton", AssetBundleName = "myskeleton", AssetBundleUri = "https://mraiwebapp.azurewebsites.net/AssetBundles/", positionX =0f, positionY = 0f, positionZ = 0f, rotationX = 0f, rotationY = 0f, rotationZ = 0f, scaleX = 3f, scaleY = 3f, scaleZ = 3f }
    };
    public static List<ObjectModel> GetObjectList()
    {
        return objectModelItems;
    }
} 


