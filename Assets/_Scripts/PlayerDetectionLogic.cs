using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionLogic : MonoBehaviour {
    
    //public GameObject ConnectionNoise;
    public bool detection = false;
    public string direction;
    public List<Collider2D> activeColliders= new List<Collider2D>();
    private Collider2D thisColllider;

    private GameObject connectionNoise;
    private GameObject disconnectNoise;

    void Start()
    {
        connectionNoise = GameObject.FindGameObjectWithTag("ConnectionNoise");
        disconnectNoise = GameObject.FindGameObjectWithTag("DisconnectNoise");

        thisColllider = GetComponent<Collider2D>();

        switch (direction)
        {
            case "Up":
                gameObject.SetActive(transform.parent.parent.GetComponentInChildren<PlayerCircuitLogic>().upCircuitPresent);
                break;
            case "Right":
                gameObject.SetActive(transform.parent.parent.GetComponentInChildren<PlayerCircuitLogic>().rightCircuitPresent);
                break;
            case "Down":
                gameObject.SetActive(transform.parent.parent.GetComponentInChildren<PlayerCircuitLogic>().downCircuitPresent);
                break;
            case "Left":
                gameObject.SetActive(transform.parent.parent.GetComponentInChildren<PlayerCircuitLogic>().leftCircuitPresent);
                break;
        }

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {

        ColliderLogic(other, true);

        if (detection)
        {
            Debug.Log("Connection noise play");

            AudioSource audio = connectionNoise.GetComponent<AudioSource>();
            audio.Play();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        ColliderLogic(collision, false);

    }

    private void ColliderLogic(Collider2D other, bool enter)
    {
        if (other.tag == "Player" || other.tag == "ConductingBlock")
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
                other.GetComponent<PowerSource>().Energise(thisColllider);
            }
        }        

    }


    void OnTriggerExit2D(Collider2D other)
    {
        //detectionCounter--;
        RemoveFromColliderList(other);

        if (other.tag == "PowerSource")
        {
            other.GetComponent<PowerSource>().UnEnergise(thisColllider);
        }

            if (activeColliders.Count==0)
        {
            AudioSource audio = disconnectNoise.GetComponent<AudioSource>();
            audio.Play();

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
