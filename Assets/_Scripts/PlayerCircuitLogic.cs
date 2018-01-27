using UnityEngine;

public class PlayerCircuitLogic : MonoBehaviour {

    private GameObject upCircuit;
    private GameObject rightCircuit;
    private GameObject downCircuit;
    private GameObject leftCircuit;

    public bool upCircuitPresent;
    public bool rightCircuitPresent;
    public bool downCircuitPresent;
    public bool leftCircuitPresent;

    public bool upEnergised;
    public bool rightEnergised;
    public bool downEnergised;
    public bool leftEnergised;

    public bool circuitEnergised;

    public Material nonEnergisedMaterial;
    public Material energisedMaterial;

    public GameObject upDetector;
    public GameObject rightDetector;
    public GameObject downDetector;
    public GameObject leftDetector;

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

        upCircuit.SetActive(upCircuitPresent);
        rightCircuit.SetActive(rightCircuitPresent);
        downCircuit.SetActive(downCircuitPresent);
        leftCircuit.SetActive(leftCircuitPresent);

    }

    // Use this for initialization
    void Start () {

        UpdateCircuitGraphics();

    }
	
	// Update is called once per frame
	void Update () {              

        upEnergised = upDetector.GetComponent<PlayerDetectionLogic>().detection;
        rightEnergised = rightDetector.GetComponent<PlayerDetectionLogic>().detection;
        downEnergised = downDetector.GetComponent<PlayerDetectionLogic>().detection;
        leftEnergised = leftDetector.GetComponent<PlayerDetectionLogic>().detection;

        circuitEnergised = (upCircuitPresent && upEnergised) || (rightCircuitPresent && rightEnergised) || (downCircuitPresent && downEnergised) || (leftCircuitPresent && leftEnergised);
            

        SetMaterialBasedOnEnergisation(upCircuit, circuitEnergised);
        SetMaterialBasedOnEnergisation(rightCircuit, circuitEnergised);
        SetMaterialBasedOnEnergisation(downCircuit, circuitEnergised);
        SetMaterialBasedOnEnergisation(leftCircuit, circuitEnergised);

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
