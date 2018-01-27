using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour {
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public double xvel;
    public double yvel;

    // Use this for initialization
    void Start () {
        
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        xvel = rb.velocity.x;
        yvel = rb.velocity.y;

        if (rb.velocity.y == 0)
        {
            anim.SetBool("jumping", false);
        }
        else
        {
            anim.SetBool("jumping", true);
        }

        if (rb.velocity.x < 0)
        {
            anim.SetBool("walking", true);
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            anim.SetBool("walking", true);
            sr.flipX = false;
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
}
