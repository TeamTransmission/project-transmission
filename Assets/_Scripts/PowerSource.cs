using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour {

    public bool energised;
    public bool levelGoal;

    private GameObject manager;

	// Use this for initialization
	void Start ()
    {	
            transform.GetChild(0).gameObject.SetActive(energised);

        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void Energise()
    {

        energised = true;
        transform.GetChild(0).gameObject.SetActive(true);

        if(levelGoal)
        {
            
        }

    }
	
}
