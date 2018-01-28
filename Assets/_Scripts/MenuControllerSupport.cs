using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerSupport : MonoBehaviour {
     
    private bool controllerSupportActive = false;

    private GameObject playButton;
    private GameObject quitButton;
    private GameObject audioButton;
    private GameObject helpButton;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("xbox button a") || Input.GetButtonDown("xbox button start") || Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalGamePad") != 0)
        {

            //if this is the first time this runs, find the button gameObjects
            if(!controllerSupportActive)
            {
                //playButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
            }

            Debug.Log("Button press");
            controllerSupportActive = true;



        }


    }
}
