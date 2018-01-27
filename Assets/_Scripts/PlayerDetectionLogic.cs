using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionLogic : MonoBehaviour {
    
    //public GameObject ConnectionNoise;
    private bool detection = false;
    public string direction;
    
    void Start()
    {
        //ConnectionNoise = GameObject.FindGameObjectWithTag("CoinCollectNoise");
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            PlayerCircuitLogic otherPlayerCircuit = other.GetComponentInChildren<PlayerCircuitLogic>();

            switch(direction)
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


            if (detection)
            {
                //AudioSource audio = ConnectionNoise.GetComponent<AudioSource>();
                //audio.Play();
            }
            
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        detection = false;
    }

}
