using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour {

    public bool energised;
    public bool levelGoal;

    Animator anim;
    SpriteRenderer sr;

    private GameObject manager;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        anim.SetBool("active", false);
        
        manager = GameObject.FindGameObjectWithTag("Manager");

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        anim.SetBool("active", false);
    }

    public void Energise()
    {

        energised = true;
        
        if(levelGoal)
        {
            manager.GetComponent<LevelComplete>().ActivateLevelComplete();
        }

    }

    void Update()
    {
        if (energised)
        {
            anim.SetBool("active", true);
        }
        else
        {
            anim.SetBool("active", false);
        }
    }

}
