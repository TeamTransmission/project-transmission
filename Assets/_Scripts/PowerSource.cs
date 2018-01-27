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

        transform.GetChild(0).gameObject.SetActive(energised);

        manager = GameObject.FindGameObjectWithTag("Manager");

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        anim.SetBool("active", false);
    }

    public void Energise()
    {

        energised = true;
        transform.GetChild(0).gameObject.SetActive(true);

        if(levelGoal)
        {
            
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
