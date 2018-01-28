using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour {

    public bool energised;
    public bool levelGoal;

    public List<Collider2D> activeColliders = new List<Collider2D>();

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

    public void Energise(Collider2D collider)
    {

        energised = true;
        
        if(levelGoal)
        {
            manager.GetComponent<LevelComplete>().ActivateLevelComplete();
        }

    }

    public void unEnergise()
    {

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
