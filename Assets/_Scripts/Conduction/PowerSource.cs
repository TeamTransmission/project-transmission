using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : ConductingObject {

    public bool startBlock;
    public bool energised;    

    public List<Collider2D> activeColliders = new List<Collider2D>();

    private GameObject[] generators;
    private int generatorCount;

    Animator anim;
    SpriteRenderer sr;

    private GameObject manager;

    private GameObject humNoise;

    public bool staysEnergised; //temp fix to stop game breaking on Level01 and 02

    // Use this for initialization
    void Start ()
    {

        upCircuitPresent = true;
        rightCircuitPresent = true;
        downCircuitPresent = true;
        leftCircuitPresent = true;

        humNoise = GameObject.FindGameObjectWithTag("HumNoise");

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

        AudioSource audio = humNoise.GetComponent<AudioSource>();
        audio.Play();

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

        for(int i =0;i< activeColliders.Count; i++)
        {
            //this doesn't quite work yet since Player still thinks they should be energised
            if (!activeColliders[i].gameObject.GetComponentInChildren<PlayerCircuitLogic>().circuitEnergised)
            {
                activeColliders.Remove(activeColliders[i]);
            }
        }

        if(activeColliders.Count ==0 && !startBlock)
        {
            if (!staysEnergised)
            {
                energised = false;

                AudioSource audio = humNoise.GetComponent<AudioSource>();
                audio.Stop();
            }

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
