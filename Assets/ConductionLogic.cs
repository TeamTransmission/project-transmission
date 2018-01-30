using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductionLogic : MonoBehaviour {

    public List<ConductingObject> conductingObjects = new List<ConductingObject>();

    private GameObject startBlock;

    // Use this for initialization
    void Start () {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] generators = GameObject.FindGameObjectsWithTag("PowerSource");
        GameObject[] conductingBlocks = GameObject.FindGameObjectsWithTag("ConductingBlock");

        for(int i = 0;i<players.Length;i++)
        {
            PlayerCircuitLogic script = players[i].GetComponentInChildren<PlayerCircuitLogic>();
            conductingObjects.Add(script);
        }

        int startBlockCount = 0;

        for (int i = 0; i < generators.Length; i++)
        {

            PowerSource script = generators[i].GetComponent<PowerSource>();

            if(script.startBlock)
            {
                startBlock = generators[i];
                startBlockCount++;                
            }
            else
            {
                conductingObjects.Add(script);
            }

        }

        for (int i = 0; i < conductingBlocks.Length; i++)
        {

            PlayerCircuitLogic script = conductingBlocks[i].GetComponentInChildren<PlayerCircuitLogic>();

            conductingObjects.Add(script);

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
