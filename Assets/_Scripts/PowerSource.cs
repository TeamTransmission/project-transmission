using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour {

    public bool startBlock;
    public bool energised;    

    public List<Collider2D> activeColliders = new List<Collider2D>();

    private GameObject[] generators;
    private int generatorCount;

    Animator anim;
    SpriteRenderer sr;

    private GameObject manager;

	// Use this for initialization
	void Start ()
    {

        generators = GameObject.FindGameObjectsWithTag("PowerSource");

        generatorCount = generators.Length;

        energised = startBlock;

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

        bool levelComplete = true;

        for(int i=0; i < generatorCount; i++)
        {
            if (!generators[i].GetComponent<PowerSource>().energised)
                {
                levelComplete = false;
            }
        }

        if(levelComplete)
        {
            manager.GetComponent<LevelComplete>().ActivateLevelComplete();
        }

        AddToColliderList(collider);

    }

    public void UnEnergise(Collider2D collider)
    {
        RemoveFromColliderList(collider);

        if(activeColliders.Count ==0 && !startBlock)
        {
            energised = false;
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

    void AddToColliderList(Collider2D coll)
    {
        bool alreadyInList = false;

        for (int i = 0; i < activeColliders.Count; i++)
        {
            if (activeColliders[i] = coll)
            {
                alreadyInList = true;
            }
        }

        if (!alreadyInList)
        {
            activeColliders.Add(coll);
        }
    }

    void RemoveFromColliderList(Collider2D coll)
    {

        for (int i = 0; i < activeColliders.Count; i++)
        {
            if (activeColliders[i] = coll)
            {
                activeColliders.Remove(activeColliders[i]);
            }
        }

    }

}
