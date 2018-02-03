using UnityEngine;
using System.Collections;

public class PlayerParenting : MonoBehaviour {

    private GameObject player;

    // Use this for initialization
    void Start () {

        player = GameObject.Find("Player"); //Player's Character

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //If character collides with the platform, make it its child.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Here we must detect which direction the Player has momentum. Only MakeChild if the Player is travelling downwards!

        if (other.gameObject.tag == "Player")
        {           
            MakeChild(other.gameObject);            
        }
        else if (other.gameObject.tag == "Crate")
                {            
            
            MakeChild(other.gameObject);
            
        }
    }
    //Once it leaves the platform, become a normal object again.
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ReleaseChild(other.gameObject);
        }
    }

    void MakeChild(GameObject child)
    {
        child.transform.parent = transform;
    }

    void ReleaseChild(GameObject child)
    {
        child.transform.parent = null;       

    }

}
