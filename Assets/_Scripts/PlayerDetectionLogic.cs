using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionLogic : MonoBehaviour {
    
    //public GameObject ConnectionNoise;
    public bool detection = false;
    public string direction;
    private int detectionCounter = 0;
    
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
                    detection = otherPlayerCircuit.upCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;

                case "Right":
                    detection = otherPlayerCircuit.rightCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;

                case "Down":
                    detection = otherPlayerCircuit.downCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;

                case "Left":
                    detection = otherPlayerCircuit.leftCircuitPresent && otherPlayerCircuit.circuitEnergised;
                    break;
            }

            if (enter)
            {
                detectionCounter++;
            }


        }
        else if (other.tag == "PowerSource")
        {
            //Debug.Log("Collision detected to PowerSource");

            if (enter)
            {
                detectionCounter++;
            }

            detection = true;
        }        

    }


    void OnTriggerExit2D(Collider2D other)
    {
        detectionCounter--;
        if (detectionCounter < 1)
        {
            detection = false;
        }
    }

}
