using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : ConductingObject {

    public bool startBlock;      
        
    Animator anim;
    SpriteRenderer sr;

    private GameObject manager;

    private GameObject humNoise;
    
    public static int totalPowerSources;
    public static int energisedSourcesCounter;

    private bool levelComplete = false;

    // Use this for initialization
    void Start ()
    {

        totalPowerSources++;

        upCircuitPresent = true;
        rightCircuitPresent = true;
        downCircuitPresent = true;
        leftCircuitPresent = true;

        humNoise = GameObject.FindGameObjectWithTag("HumNoise");
        
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        anim.SetBool("active", false);
        
        manager = GameObject.FindGameObjectWithTag("Manager");

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        anim.SetBool("active", false);        

    }
    
        
    void Update()
    {

        //this could be part of @Override GetPowered() instead to be more efficient
        if (powered)
        {
            anim.SetBool("active", true);
        }
        else
        {
            anim.SetBool("active", false);
        }
    }
    
    public override void SetPowered(bool state)
    {       

        if(state && ! this.GetPowered())
        {
            energisedSourcesCounter++;
        }
        else if (!state && this.GetPowered())
        {
            energisedSourcesCounter--;
        }

        base.SetPowered(state);

        if(energisedSourcesCounter == totalPowerSources && !levelComplete)
        {
            manager.GetComponent<LevelComplete>().ActivateLevelComplete();
            levelComplete = true;
        }

    }

}
