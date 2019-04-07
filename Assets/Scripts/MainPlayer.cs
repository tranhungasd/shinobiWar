using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    private void HandleMovement(float horizontal)
    {
        Flip(horizontal);
        myRigidbody2D.velocity = new Vector2(horizontal * speed, myRigidbody2D.velocity.y);
        //Debug.Log(horizontal);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        //if(jump==true) myAnimator.SetBool("Jump", true);
        //if (jump == false) myAnimator.SetBool("Jump", false);
        //if (slide == false) myAnimator.SetBool("Slide", false);
        if (jump == true) setJump();

    }
    private void Flip(float horizontal)
    {
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    private void GetInput()
    {
        direction = Vector2.zero;
        setIdle();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
            setRun();
            if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
            {
                StartCoroutine(SlideController());
                slide = false;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
            setRun();
            if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
            {
                StartCoroutine(SlideController());
                slide = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            jump = true;
            grounded = false;
        }

    }
    private IEnumerator SlideController()
    {
        float timePassed = 0;
        while (timePassed < 1)
        {
            //myAnimator.SetBool("Slide", true);
            setCDN();
            timePassed += Time.deltaTime;

            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            grounded = true;
            jump = false;
        }
    }
    private void setCDN()
    {
        myAnimator.SetFloat("cdn", 1);
        myAnimator.SetFloat("idle", 0);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 0);
    }
    private void setRun()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("idle", 0);
        myAnimator.SetFloat("run", 1);
        myAnimator.SetFloat("jump", 0);
    }
    private void setJump()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("idle", 0);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 1);
    }
    private void setIdle()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("idle", 1);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 0);
    }
}