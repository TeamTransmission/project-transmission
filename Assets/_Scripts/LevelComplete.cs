using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour {

    private GameObject canvas;

    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }
		
	public void ActivateLevelComplete ()
    {
        canvas.GetComponentInChildren<Text>().enabled = true;
	}
}
