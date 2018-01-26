using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircuitLogic : MonoBehaviour {

    public GameObject upCircuit;
    public GameObject rightCircuit;
    public GameObject downCircuit;
    public GameObject leftCircuit;

    public bool upCircuitPresent;
    public bool rightCircuitPresent;
    public bool downCircuitPresent;
    public bool leftCircuitPresent;

    public bool upCircuitEnergised;
    public bool rightCircuitEnergised;
    public bool downCircuitEnergised;
    public bool leftCircuitEnergised;

    // Use this for initialization
    void Start () {

        upCircuit = transform.GetChild(0).gameObject;
        rightCircuit = transform.GetChild(1).gameObject;
        downCircuit = transform.GetChild(2).gameObject;
        leftCircuit = transform.GetChild(3).gameObject;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
