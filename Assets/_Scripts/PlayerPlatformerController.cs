﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Chronos;

public class PlayerPlatformerController : PhysicsObject
{

    Animator anim;
    SpriteRenderer sr;

    public bool thisCharacterIsActive;

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    
    public float cycleTime = 0.5f;
    public float cycleAngle = 45f;

    public float tempCycleValue;

    public bool gamepadAttached;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
       string[] joystickNames = Input.GetJoystickNames();

        //Debug.Log("Joysticks attached = " + joystickNames.Length);

        gamepadAttached = !(joystickNames.Length == 0);        

    }

    protected override void ComputeVelocity()
    {


        Vector2 move = Vector2.zero;

        move.x = 1;

        if (grounded)
        {
            anim.SetBool("jumping", false);
        }
        if (velocity.x < 0)
        {
            anim.SetBool("walking", true);
            sr.flipX = true;
        }
        else if (velocity.x > 0)
        {
            anim.SetBool("walking", true);
            sr.flipX = false;
        }
        else
        {
            anim.SetBool("walking", false);
        }

        if (thisCharacterIsActive)
        {
            if (gamepadAttached)
                move.x = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalGamePad");
            else
                move.x = Input.GetAxis("Horizontal");
                    
            anim.SetBool("locked", false);

        }
        else
        {
            move.x = 0;
            anim.SetBool("locked", true);
        }

        if (thisCharacterIsActive && Input.GetButtonDown("xbox button a") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;

            anim.SetBool("jumping", true);

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

        }
        else if (thisCharacterIsActive && Input.GetButtonUp("xbox button a"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }

        targetVelocity = move * maxSpeed;


    }
}

    





