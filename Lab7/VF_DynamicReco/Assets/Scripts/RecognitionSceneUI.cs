using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using AssetBundles;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene("PositionScene");
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
