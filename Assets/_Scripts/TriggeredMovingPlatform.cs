using UnityEngine;
using System.Collections;

public class TriggeredMovingPlatform : MonoBehaviour
{

    private GameObject player;

    public int Marker = 0;
    private int WayPointLength;
    public int speed = 4;
    public Transform[] Waypoints;
    public GameObject Trigger;

    public bool ContinuouslyMoving = false;

    void Start()
    {
        WayPointLength = Waypoints.Length;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, Waypoints[Marker].transform.position, speed * Time.deltaTime);


        if (!ContinuouslyMoving)
        {

            if (transform.position == Waypoints[Marker].transform.position & Trigger.GetComponent<PowerSource>().energised == true)
            {
                if (Marker < WayPointLength - 1)
                {
                    Marker++;
                }
            }
            if (Marker == Waypoints.Length - 1 & Trigger.GetComponent<PowerSource>().energised == false)
            {
                Marker = 0;
            }
        }
        else
        {
            if (Trigger.GetComponent<PowerSource>().energised == true)
            {
                if (transform.position == Waypoints[Marker].transform.position)
                {
                    Marker++;
                }
                if (Marker == Waypoints.Length)
                {
                    Marker = 0;
                }

            }
        }
    }
}





