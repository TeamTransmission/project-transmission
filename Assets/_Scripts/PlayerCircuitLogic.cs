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

    public bool upCircuitEnergised;
    public bool rightCircuitEnergised;
    public bool downCircuitEnergised;
    public bool leftCircuitEnergised;

    public Material nonEnergisedMaterial;
    public Material energisedMaterial;

    // Use this for initialization
    void Start () {

        upCircuit = transform.GetChild(0).gameObject;
        rightCircuit = transform.GetChild(1).gameObject;
        downCircuit = transform.GetChild(2).gameObject;
        leftCircuit = transform.GetChild(3).gameObject;

        upCircuit.SetActive(upCircuitPresent);
        rightCircuit.SetActive(rightCircuitPresent);
        downCircuit.SetActive(downCircuitPresent);
        leftCircuit.SetActive(leftCircuitPresent);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
