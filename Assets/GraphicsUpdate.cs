using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsUpdate : MonoBehaviour {

    /// Add a context menu named "Do Something" in the inspector
    /// of the attached script.
    [ContextMenu("UpdatePlayerGraphics")]

    // Use this for initialization
    void UpdatePlayerGraphics () {

        GetComponentInChildren<PlayerCircuitLogic>().UpdateCircuitGraphics();


    }
	
}
