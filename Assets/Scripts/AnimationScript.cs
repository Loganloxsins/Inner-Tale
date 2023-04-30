using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    private Animator anim;
    private Movement move;
    private Collision coll;
    private Dead death;
    [HideInInspector]
    public SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        move = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
        death = GetComponent<Dead>();
    }

    void Update()
    {
        // Debug.Log("anime");
        //anim.SetTrigger("toDie", FindObjectOfType<Dead>())
        anim.SetBool("isDead", FindObjectOfType<Dead>().isDead);
        anim.SetBool("isTalking", FindObjectOfType<Movement>().isTalking);
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);
        anim.SetBool("onRightWall", coll.onRightWall);
        anim.SetBool("wallGrab", move.wallGrab);
        anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isDashing", move.isDashing);

    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        //Debug.Log("sethoranmie");
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {
        //Debug.Log("turn");
        if (move.wallGrab || move.wallSlide)
        {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }
}
