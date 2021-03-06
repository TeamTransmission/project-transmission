﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected BoxCollider2D playerBoxCollider;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();

    }

    void Start()
    {
        contactFilter.useTriggers = false;        

        //try putting a breakpoint here and investigating
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            //int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);            
            int count = playerBoxCollider.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();
            
            for (int i = 0; i < count; i++)
            {
                
                if(hitBuffer[i].collider.isTrigger == false)
                    {
                    switch (hitBuffer[i].transform.gameObject.layer)
                    {

                        case 16:;  //16 is the one way platfrom layer
                            if (hitBuffer[i].transform.position.y < gameObject.transform.position.y - 0.4)
                            {
                                hitBufferList.Add(hitBuffer[i]);
                            }
                            break;
                        case 17:;  //17 is the pass through player layer
                                   //do nothing
                            break;
                        default:
                            hitBufferList.Add(hitBuffer[i]);
                            break;
                    }
                }
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}