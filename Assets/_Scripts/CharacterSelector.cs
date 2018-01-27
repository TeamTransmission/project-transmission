using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
        //get list of players
        //won't work if we get all objects with tag player since it will grab child objects too

	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("xbox button lb"))
        {

        }

        if (Input.GetButtonDown("xbox button rb"))
        {

        }

    }
}
