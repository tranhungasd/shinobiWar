using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainPlayer : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private Vector2 direction;
    public float jumpHeight;
    [SerializeField]
    private float speed = 4;
    private bool facingRight;
    private bool grounded = true;
    private bool wallcling = false;
    public AudioSource runAudio;
    public AudioSource jumpAudio;
    public AudioSource downButtonAudio;
    bool jump = false;
    bool slide = false;
    double jumptime = 1.0;
    void Start()
    {
        facingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jump = false;
        slide = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void HandleMovement(float horizontal)
    {
        Flip(horizontal);
        myRigidbody2D.velocity = new Vector2(horizontal * speed, myRigidbody2D.velocity.y);
        if (jump == true) setJump();

    }
    private void Flip(float horizontal)
    {
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            //myAnimator.SetBool("move", true);
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            Vector3 thePos = transform.localPosition;
            if (Int32.Parse(theScale.x.ToString()) < 0)
            {
                thePos.x -= 1.9f;
                //Debug.Log(theScale.x.ToString() + " : " + thePos.x.ToString());
            }
            else
            {
                thePos.x += 1.9f;
                //Debug.Log(theScale.x.ToString() + " : " + thePos.x.ToString());
            }
            transform.localPosition = thePos;
            transform.localScale = theScale;
        }
    }
    private void GetInput()
    {
        direction = Vector2.zero;
        //myAnimator.SetBool("move", false);
        if (!wallcling && grounded) myAnimator.SetBool("move", false);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            runAudio.Play();
            myAnimator.SetBool("move", true);
            UnfreezePosition();
            direction += Vector2.left;
            if (wallcling && facingRight) setJump();
            if (grounded) setRun();

            if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
            {
                downButtonAudio.Play();
                StartCoroutine(SlideController());
                slide = false;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            runAudio.Play();
            myAnimator.SetBool("move", true);
            UnfreezePosition();
            direction += Vector2.right;
            if (wallcling && !facingRight) setJump();
            if (grounded) setRun();
            if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
            {
                downButtonAudio.Play();
                StartCoroutine(SlideController());
                slide = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && (grounded || wallcling))
        {
<<<<<<< HEAD
=======
            jumpAudio.Play();
            myAnimator.SetBool("move", true);
            UnfreezePosition();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
>>>>>>> 30a7e74f9976ddbf6da08667e6651cc067e1a3bd
            jump = true;
            wallcling = false;
            grounded = false;
            myAnimator.SetBool("move", true);
            UnfreezePosition();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            
        }

    }
    private IEnumerator SlideController()
    {
        float timePassed = 0;
        while (timePassed < 1)
        {
            //myAnimator.SetBool("Slide", true);
            if (Input.GetKeyDown(KeyCode.UpArrow)) break;
            setCDN();
            timePassed += Time.deltaTime;

            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            wallcling = false;
            grounded = true;
            jump = false;
        }
        if((other.gameObject.tag == "wall") && !grounded)
        {
            wallcling = true;
            jump = false;
            FreezePosition();
            setWallClinging();
        }
    }
    private void FreezePosition()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }
    private void UnfreezePosition()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
    private void setCDN()
    {
        myAnimator.SetFloat("cdn", 1);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 0);
        myAnimator.SetFloat("wallcling", 0);
    }
    private void setRun()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("run", 1);
        myAnimator.SetFloat("jump", 0);
        myAnimator.SetFloat("wallcling", 0);
    }
    private void setJump()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 1);
        myAnimator.SetFloat("wallcling", 0);
    }
    private void setIdle()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 0);
        myAnimator.SetFloat("wallcling", 0);
    }
    private void setWallClinging ()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 0);
        myAnimator.SetFloat("wallcling", 1);
    }
}