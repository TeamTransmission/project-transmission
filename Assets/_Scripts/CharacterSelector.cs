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

        //temp character selector
        activePlayer = players[0];
        CharacterActivator();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("xbox button rb"))
        {
            if (activePlayer == null)
            {
                activePlayerNumber = 0;
            }
            else
            {

                SnapCurrentPlayer();

                if (activePlayerNumber < playerCount -1)
                {
                    activePlayerNumber++;
                }
                else
                {
                    activePlayerNumber = 0;
                }
            }

            activePlayer = players[activePlayerNumber];
            CharacterActivator();
        }

        if (Input.GetButtonDown("xbox button lb"))
        {
            if (activePlayer == null)
            {
                activePlayerNumber = 0;
            }
            else
            {

                SnapCurrentPlayer();

                if (activePlayerNumber > 0)
                {
                    activePlayerNumber--;
                }
                else
                {
                    activePlayerNumber = playerCount - 1;
                }
            }

            activePlayer = players[activePlayerNumber];
            CharacterActivator();
        }

    }

    void CharacterActivator()
    {
        for(int i = 0;i<playerCount; i++)
        {            
                players[i].GetComponent<PlayerPlatformerController>().thisCharacterIsActive = (i == activePlayerNumber);            
        }
    }

    private void SnapCurrentPlayer()
    {
        activePlayer.transform.position = new Vector3(Mathf.RoundToInt(activePlayer.transform.position.x), activePlayer.transform.position.y, 0);
    }

}



