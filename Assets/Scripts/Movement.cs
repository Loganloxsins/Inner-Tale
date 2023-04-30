using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class Movement : MonoBehaviour
{

    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;
    private Animator myAnim;

    [Space]
    [Header("Stats")]
    public float speed = 5;
    public float jumpForce = 20;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove = true;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    public bool isTalking = false;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;


    [SerializeField] private AudioSource walkSoundEffect;
   
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;
    bool isrun;
    AudioSource walkreally;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        walkreally = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        isrun = false;
        if (!isTalking)
        {
            Walk(dir);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        /*if (coll.onWall)
        {
            walkSoundEffect.Stop();
        }
        else {
            if (isrun == true && (!walkreally.isPlaying) && (!isDashing) && (groundTouch))
                walkSoundEffect.Play();
            if ((isrun == false || groundTouch == false || isDashing) && coll.onWall == false)
                walkSoundEffect.Stop();
        }
*/
        if (isrun == true && (!walkreally.isPlaying) && (!isDashing) && (groundTouch))
            walkSoundEffect.Play();
        if ((isrun == false || groundTouch == false || isDashing) && coll.onWall == false)
            walkSoundEffect.Stop();
        anim.SetHorizontalMovement(x, y, rb.velocity.y);
        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 28;
        }

        if (coll.onWall && !coll.onGround)
        {
          //  Debug.Log(rb.velocity.y);
            if (x != 0 && !wallGrab&&(rb.velocity.y<0))
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");
           
            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0) {
                Dash(xRaw, yRaw);

            }
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }



        if (wallGrab || wallSlide || !canMove)
            return;

        if (x > 0&& !isTalking)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0&& !isTalking)
        {
            side = -1;
            anim.Flip(side);
        }


    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        side = anim.sr.flipX ? -1 : 1;

    }

    private void Dash(float x, float y)
    {

        // Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        //FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;
        anim.SetTrigger("dash");
         Debug.Log("dash");
        

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
       StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
      //  Debug.Log("dashwait");
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;
        dashSoundEffect.Play();
        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
        dashSoundEffect.Stop();
    }

    IEnumerator GroundDash()
    {
        Debug.Log("grounddash");
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        jumpSoundEffect.Play();
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if (coll.wallSide != side)
            anim.Flip(side*-1);

        if (!canMove)
                return;

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        if (isTalking)
        {
            rb.velocity = Vector2.zero;
        }
        if (wallGrab)
            return;
        bool PlayerHasAxisSpeed = Mathf.Abs(rb.velocity.x) > 0.01;
        isrun = PlayerHasAxisSpeed;
       
        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

            myAnim.SetBool("isRun", PlayerHasAxisSpeed);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;


    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    void Flip()
    {
        
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

}



