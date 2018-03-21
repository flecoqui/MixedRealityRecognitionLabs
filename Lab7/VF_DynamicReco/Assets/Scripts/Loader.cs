using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject stateManager;
	// Use this for initialization
	void Awake () {
 
        if (StateManager.instance == null)
        {
            Instantiate(stateManager);
        }
    }

}
