using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public GameObject[] players;
    public GameObject activePlayer;

    public int activePlayerNumber;

    private int playerCount;

    // Use this for initialization
    void Start()
    {

        //get list of players
        //won't work if we get all objects with tag player since it will grab child objects too
        players = GameObject.FindGameObjectsWithTag("Player");

        playerCount = players.Length;

        Debug.Log("Player Count = " + playerCount.ToString());

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("xbox button lb"))
        {
            if (activePlayer = null)
            {
                activePlayerNumber = 0;
            }
            else
            {
                activePlayerNumber++;
            }

            activePlayer = players[activePlayerNumber];

        }

        if (Input.GetButtonDown("xbox button rb"))
        {
            if (activePlayer = null)
            {
                activePlayerNumber = 0;
            }
            else
            {
                activePlayerNumber++;
            }

            activePlayer = players[activePlayerNumber];
        }

    }
}

