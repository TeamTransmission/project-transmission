using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterSelector : MonoBehaviour
{

    //public GameObject[] players;
    public List<GameObject> players = new List<GameObject>();

    public GameObject activePlayer;

    public int activePlayerNumber;

    private int playerCount;

    // Use this for initialization
    void Start()
    {

        //get list of players
        //won't work if we get all objects with tag player since it will grab child objects too
        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");

        playerCount = playersArray.Length;

        for (int i = 0; i < playerCount; i++)
        {
            players.Add(playersArray[i]);
        }

        OrderPlayerList();

        for (int i = 0; i < playerCount; i++)
        {
            //Debug.Log(players[i].transform.position.x);
        }

        //Debug.Log("Player Count = " + playerCount.ToString());

        //temp character selector
        SetDefaultPlayer();
        CharacterActivator();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("xbox button rb"))
        {

            OrderPlayerList();
            activePlayerNumber = GetNumberOfCurrentlyActivePlayer();

            if (activePlayer == null)
            {
                SetDefaultPlayer();
            }
            else
            {

                SnapCurrentPlayer();

                if (activePlayerNumber < playerCount - 1)
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

            OrderPlayerList();
            activePlayerNumber = GetNumberOfCurrentlyActivePlayer();

            if (activePlayer == null)
            {
                SetDefaultPlayer();
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
        for (int i = 0; i < playerCount; i++)
        {
            players[i].GetComponent<PlayerPlatformerController>().thisCharacterIsActive = (i == activePlayerNumber);
        }
    }

    private void SnapCurrentPlayer()
    {
        activePlayer.transform.position = new Vector3(Mathf.RoundToInt(activePlayer.transform.position.x), activePlayer.transform.position.y, 0);
    }

    private void SetDefaultPlayer()
    {
        activePlayerNumber = playerCount - 1;
        activePlayer = players[activePlayerNumber];
    }

    private void OrderPlayerList()
        {
        List<GameObject> playersTemp = new List<GameObject>();
    playersTemp = players.OrderBy(plyr => plyr.transform.position.x).ToList();
    players = playersTemp;
        }

    private int GetNumberOfCurrentlyActivePlayer()
    {
        for(int i=0 ; i<playerCount;i++)
        {
            if(activePlayer==players[i])
            {
                return i;
            }
        }

        return 0;

    }

}



