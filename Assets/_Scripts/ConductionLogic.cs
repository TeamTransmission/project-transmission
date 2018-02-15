using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductionLogic : MonoBehaviour {

    private List<ConductingObject> conductingObjects = new List<ConductingObject>();
    private List<ConductingObject> unEnergisedConductors;

    private ConductingObject startBlock;

    private GameObject connectionNoise;
    private GameObject disconnectNoise;

    int counter=0;

    private double connectionDistance = 1.2; //distance that characters need to be within in order to make a connection

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
                startBlock.SetPowered(true);
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
            if (script != null)
            {
                conductingObjects.Add(script);
            }      

        }

    }
	
	// Update is called once per frame
	void Update () {

        counter++;

        //make this run every few frames
        if(counter==10)
        {
            unEnergisedConductors = new List<ConductingObject>(conductingObjects);
            CheckForContact(startBlock);

            //Unpower all objects remaining in unEnergisedConductors list
            for (int i = 0; i < unEnergisedConductors.Count; i++)
            {

                if (unEnergisedConductors[i].GetPowered())
                { 
                    AudioSource audio = disconnectNoise.GetComponent<AudioSource>();
                    audio.Play();
                }

                unEnergisedConductors[i].SetPowered(false);
            }

            counter = 0;
        }

    }

    void CheckForContact(ConductingObject conductor)
    {

        List<ConductingObject> listToLoopThrough = new List<ConductingObject>(unEnergisedConductors);

        for (int i=0;i< listToLoopThrough.Count; i++)
        {   

            if(Vector2.Distance(conductor.transform.position, listToLoopThrough[i].transform.position)< connectionDistance)
                {

                ConductingObject newEnergisedConductor = listToLoopThrough[i];

                if (AreConductorsCompatible(conductor,newEnergisedConductor))
                {

                    FindAndRemoveFromUnErergisedConductors(newEnergisedConductor);               
                    
                    if (!newEnergisedConductor.GetPowered())
                    {
                        AudioSource audio = connectionNoise.GetComponent<AudioSource>();
                        audio.Play();
                    }

                    newEnergisedConductor.SetPowered(true);

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
                return conductorA.GetLeftCircuitPresent() && conductorB.GetRightCircuitPresent();
            }
            else //a left of b
            {
                return conductorA.GetRightCircuitPresent() && conductorB.GetLeftCircuitPresent();
            }
        }
        else //must be above or below
        {
            if (yOffset>0) //a above b
            {
                return conductorA.GetDownCircuitPresent() && conductorB.GetUpCircuitPresent();
            }
            else //a below b
            {
                return conductorA.GetUpCircuitPresent() && conductorB.GetDownCircuitPresent();
            }
        }
        
    }

    void FindAndRemoveFromUnErergisedConductors(ConductingObject conductor)
    {
        for(int i=0;i<unEnergisedConductors.Count;i++)
        {
            if(conductor == unEnergisedConductors[i])
            {
                unEnergisedConductors.RemoveAt(i);
                break;
            }
        }
    }

}
