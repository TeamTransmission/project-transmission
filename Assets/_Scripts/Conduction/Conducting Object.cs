using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductingObject : MonoBehaviour {
    
    protected bool powered;

    protected bool upCircuitPresent;
    protected bool rightCircuitPresent;
    protected bool downCircuitPresent;
    protected bool leftCircuitPresent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public bool GetPowered()
    {
        return this.powered;
    }

    public virtual void SetPowered(bool state)
    {
        this.powered = state;
    }

    public bool GetUpCircuitPresent()
    {
        return upCircuitPresent;
    }

    public bool GetRightCircuitPresent()
    {
        return rightCircuitPresent;
    }

    public bool GetDownCircuitPresent()
    {
        return downCircuitPresent;
    }

    public bool GetLeftCircuitPresent()
    {
        return leftCircuitPresent;
    }

}
