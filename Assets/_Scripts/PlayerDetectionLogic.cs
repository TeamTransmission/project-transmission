using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionLogic : MonoBehaviour {
    
    //public GameObject ConnectionNoise;
    public bool detection = false;
    public string direction;
    public List<Collider2D> activeColliders= new List<Collider2D>();
    
    void Start()
    {
        //ConnectionNoise = GameObject.FindGameObjectWithTag("CoinCollectNoise");
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {

        ColliderLogic(other, true);

        if (detection)
        {
            //AudioSource audio = ConnectionNoise.GetComponent<AudioSource>();
            //audio.Play();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        ColliderLogic(collision, false);

    }

    private void ColliderLogic(Collider2D other, bool enter)
    {
        if (other.tag == "Player")
        {

            PlayerCircuitLogic otherPlayerCircuit = other.GetComponentInChildren<PlayerCircuitLogic>();

            switch (direction)
            {
                case "Up":
                    detection = otherPlayerCircuit.downCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;

                case "Right":
                    detection = otherPlayerCircuit.leftCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;

                case "Down":
                    detection = otherPlayerCircuit.upCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;

                case "Left":
                    detection = otherPlayerCircuit.rightCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;
            }

            if (enter)
            {
                //detectionCounter++;
                AddToColliderList(other);
            }


        }
        else if (other.tag == "PowerSource")
        {
            //Debug.Log("Collision detected to PowerSource");

            if (other.GetComponent<PowerSource>().energised)
            {
                if (enter)
                {
                    //detectionCounter++;
                    AddToColliderList(other);
                }

                detection = true;
            }
            else if(transform.parent.parent.GetComponentInChildren<PlayerCircuitLogic>().circuitEnergised)
            {
                //activate the checkpoint or level exit
                other.GetComponent<PowerSource>().Energise();
            }
        }        

    }


    void OnTriggerExit2D(Collider2D other)
    {
        //detectionCounter--;
        RemoveFromColliderList(other);

        if (activeColliders.Count==0)
        {
            detection = false;
        }
    }

    void AddToColliderList(Collider2D coll)
    {
        bool alreadyInList = false;

        for (int i= 0; i<activeColliders.Count; i++)
        {
            if(activeColliders[i] = coll)
            {
                alreadyInList = true;               
            }
        }

        if (!alreadyInList)
        {
            activeColliders.Add(coll);
        }
    }

    void RemoveFromColliderList(Collider2D coll)
    {

        for (int i = 0; i < activeColliders.Count; i++)
        {
            if (activeColliders[i] = coll)
            {                
                activeColliders.Remove(activeColliders[i]);
            }
        }

    }
}
