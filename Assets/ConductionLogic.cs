using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductionLogic : MonoBehaviour {

    public List<ConductingObject> conductingObjects = new List<ConductingObject>();
    public List<ConductingObject> unEnergisedConductors;

    private ConductingObject startBlock;

    private GameObject connectionNoise;
    private GameObject disconnectNoise;

    // Use this for initialization
    void Start () {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] generators = GameObject.FindGameObjectsWithTag("PowerSource");
        GameObject[] conductingBlocks = GameObject.FindGameObjectsWithTag("ConductingBlock");

        connectionNoise = GameObject.FindGameObjectWithTag("ConnectionNoise");
        disconnectNoise = GameObject.FindGameObjectWithTag("DisconnectNoise");

        for (int i = 0;i<players.Length;i++)
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

                if (unEnergisedConductors[i].powered)
                { 
                    AudioSource audio = disconnectNoise.GetComponent<AudioSource>();
                    audio.Play();
                }

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

                    if (!unEnergisedConductors[i].powered)
                    {
                        AudioSource audio = connectionNoise.GetComponent<AudioSource>();
                        audio.Play();
                    }

                    newEnergisedConductor.powered = true;

                    CheckForContact(newEnergisedConductor);
                }

            }


        }


    }

    bool AreConductorsCompatible(ConductingObject conductorA, ConductingObject conductorB)
    {

        float xOffset = conductorA.transform.position.x - conductorB.transform.position.x;
        float yOffset = conductorA.transform.position.y - conductorB.transform.position.y;

        //are the players to the side of each other?
        if(Mathf.Abs(xOffset)>Mathf.Abs(yOffset))
        {

            if(xOffset>0) //a right of b
            {
                return conductorA.leftCircuitPresent && conductorB.rightCircuitPresent;
            }
            else //a left of b
            {
                return conductorA.rightCircuitPresent && conductorB.leftCircuitPresent;
            }
        }
        else //must be above or below
        {
            if (yOffset>0) //a above b
            {
                return conductorA.downCircuitPresent && conductorB.upCircuitPresent;
            }
            else //a below b
            {
                return conductorA.upCircuitPresent && conductorB.downCircuitPresent;
            }
        }
        
    }


}
