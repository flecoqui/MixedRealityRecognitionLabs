using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
    public GameObject vuforiaManager;
    public GameObject stateManager;
	// Use this for initialization
	void Awake () {
        if(VuforiaManager.instance == null)
        {
            Instantiate(vuforiaManager);
        }
        if (StateManager.instance == null)
        {
            Instantiate(stateManager);
        }
    }

}
