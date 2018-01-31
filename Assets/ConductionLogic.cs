using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductionLogic : MonoBehaviour {

    public List<ConductingObject> conductingObjects = new List<ConductingObject>();
    public List<ConductingObject> unEnergisedConductors;

    private ConductingObject startBlock;

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
                startBlock = script;
                startBlock.powered = true;
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

        //Maybe make this run every few frames as to not be too expensive
        {
            unEnergisedConductors = new List<ConductingObject>(conductingObjects);
            CheckForContact(startBlock);

            //Unpower all objects remaining in unEnergisedConductors list
            for (int i = 0; i < unEnergisedConductors.Count; i++)
            {
                unEnergisedConductors[i].powered = false;
            }
        }

    }

    void CheckForContact(ConductingObject conductor)
    {              

        for(int i=0;i< unEnergisedConductors.Count; i++)
        {           
            if(Vector2.Distance(conductor.transform.position, unEnergisedConductors[i].transform.position)<1.2)
                {

                ConductingObject newEnergisedConductor = unEnergisedConductors[i];

                if (AreConductorsCompatible(conductor,newEnergisedConductor))
                {
                    unEnergisedConductors.RemoveAt(i);

                    //Decriment the value of i so that index doesn't go out of bounds
                    i--;

                    newEnergisedConductor.powered = true;

                    CheckForContact(newEnergisedConductor);
                }

            }


        }


    }

    bool AreConductorsCompatible(ConductingObject conductorA, ConductingObject conductorB)
    {

        bool compatible = true;

        //find out where they are positioned relative to each other

        //

        return compatible;

    }


}
