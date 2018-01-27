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

            if (true)
            {

                //logic here

                //AudioSource audio = ConnectionNoise.GetComponent<AudioSource>();
                //audio.Play();
            }
            
        }

    }

}
