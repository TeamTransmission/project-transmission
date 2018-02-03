using UnityEngine;

public class PlayerCircuitLogic : ConductingObject {

    private GameObject upCircuit;
    private GameObject rightCircuit;
    private GameObject downCircuit;
    private GameObject leftCircuit;
        
    public Material nonEnergisedMaterial;
    public Material energisedMaterial;
    
    /// Add a context menu named "Do Something" in the inspector
    /// of the attached script.
    [ContextMenu("UpdateCircuitGraphics")]

    // Use this for initialization
    public void UpdateCircuitGraphics()
    {

        upCircuit = transform.GetChild(0).gameObject;
        rightCircuit = transform.GetChild(1).gameObject;
        downCircuit = transform.GetChild(2).gameObject;
        leftCircuit = transform.GetChild(3).gameObject;

        upCircuitPresent = upCircuit.activeSelf;
        rightCircuitPresent = rightCircuit.activeSelf;
        downCircuitPresent = downCircuit.activeSelf;
        leftCircuitPresent = leftCircuit.activeSelf;

    }

    // Use this for initialization
    void Start () {

        UpdateCircuitGraphics();

    }
	
	// Update is called once per frame
	void Update () {              
        
        //this is a bit expensive to do every frame.  bool powered should be encapsolated so that set material can be part of the setter method
        SetMaterialBasedOnEnergisation(upCircuit, powered);
        SetMaterialBasedOnEnergisation(rightCircuit, powered);
        SetMaterialBasedOnEnergisation(downCircuit, powered);
        SetMaterialBasedOnEnergisation(leftCircuit, powered);
    }


    void SetMaterialBasedOnEnergisation(GameObject go, bool energised)
    {

        if (energised)
        {
            go.transform.GetComponent<Renderer>().sharedMaterial = energisedMaterial;
        }
        else
        {
            go.transform.GetComponent<Renderer>().sharedMaterial = nonEnergisedMaterial;
        }

    }
    
}
