using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEditor;
using System.IO;
using System.Text;

public class CreateAssetBundles
{

    public const string buildPath = "Assets/AssetBundles";



//    [MenuItem("Assets/Build Asset Bundles...")]

//    static void BuildMacAssetBundles()

//    {

//#if UNITY_EDITOR_WIN

//        BuildAssetBundleAndRename(BuildTarget.WSAPlayer, "WSA");

//#endif



//  //      BuildAssetBundleAndRename(BuildTarget.StandaloneWindows, "x86");

//  //      BuildAssetBundleAndRename(BuildTarget.StandaloneWindows64, "x64");



//  //      BuildAssetBundleAndRename(BuildTarget.Android, "Android");



//  //      BuildAssetBundleAndRename(BuildTarget.iOS, "iOS");

//  //      BuildAssetBundleAndRename(BuildTarget.StandaloneOSXUniversal, "OSX");

//    }



//    static private void BuildAssetBundleAndRename(BuildTarget buildTarget, string suffix)

//    {

//        if (!Directory.Exists(buildPath))
//        {

//            Debug.Log("Created build dir: " + buildPath);

//            Directory.CreateDirectory(buildPath);

//        }



//        BuildPipeline.BuildAssetBundles(buildPath, BuildAssetBundleOptions.StrictMode, buildTarget);



//        string[] files = Directory.GetFiles(buildPath);

//        foreach (string file in files)
//        {

//            if (!file.Contains(".") && !file.EndsWith("AssetBundles"))
//            {

//                string renamedBundle = file + "-" + suffix + ".unity3d";

//                // clean-up old files

//                if (File.Exists(renamedBundle))
//                {

//                    File.Delete(renamedBundle);

//                }

//                if (File.Exists(renamedBundle + ".meta"))
//                {

//                    File.Delete(renamedBundle + ".meta");

//                }

//                File.Move(file, renamedBundle);

//                if (File.Exists(file + ".meta"))
//                {

//                    File.Move(file + ".meta", renamedBundle + ".meta");

//                }

//                Debug.Log("Built bundle: " + renamedBundle);

//            }

//        }

//    }



    [MenuItem("Assets/Get Asset Bundle Names under folder Assets\\AssetBundles")]

    static void GetAssetBundleNames()

    {

        string[] names = AssetDatabase.GetAllAssetBundleNames();

        foreach (var name in names)
        {

            StringBuilder sb = new StringBuilder("AssetBundle: " + name + "\n");

            foreach (string file in Directory.GetFiles(buildPath))
            {

                string filename = Path.GetFileName(file);

                if (filename.StartsWith(name) && !filename.EndsWith(".meta") && !filename.EndsWith(".manifest"))
                {

                    sb.Append("\t" + filename);

                }

            }

            Debug.Log(sb.ToString());

        }

    }
    [MenuItem("Assets/Build AssetBundles under folder Assets\\AssetBundles for Windows")]
    static void BuildAllAssetBundles()
    {
        {
            string assetBundleDirectory = "Assets/AssetBundles/Windows";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WSAPlayer);
        }
    }


    //[MenuItem("Assets/Build AssetBundle From Selection - Track dependencies")]
    //static void ExportResurce()
    //{

    //    // Bring up save panel

    //    string basename = Selection.activeObject ? Selection.activeObject.name : "New Resource";
    //    string path = EditorUtility.SaveFilePanel("Save Resources", "", basename, "");



    //    if (path.Length != 0)
    //    {

    //        // Build the resource file from the active selection.

    //        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);



    //        // for WSA

    //        BuildPipeline.BuildAssetBundle(Selection.activeObject,

    //                                       selection, path + ".WSAPlayer.unity3d",

    //                                       BuildAssetBundleOptions.None,

    //                                       BuildTarget.WSAPlayer);





    //        Selection.objects = selection;

    //    }
    //}
}