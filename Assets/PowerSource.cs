using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour {

    public bool energised;
    bool levelGoal;

	// Use this for initialization
	void Start ()
    {	
            transform.GetChild(0).gameObject.SetActive(energised);
	}
	
}
