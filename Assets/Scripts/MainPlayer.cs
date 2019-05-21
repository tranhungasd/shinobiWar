using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainPlayer : MonoBehaviour
{
    public bool choPhepDiChuyen;
    public GameObject missionControl;
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private Vector2 direction;
    public ParameterPlayer playerStats;
    public float jumpHeight;
    public WallClingScript wallClingBox;
    private float maxHealth;
    private float curHealth;
    
    [SerializeField]
    private float speed = 4;
    private bool facingRight;
    private bool grounded = true;
    private bool wallcling = false;
    private float gravityScaleValue = 10;
    
    public AudioSource runAudio;
    public AudioSource jumpAudio;
    public AudioSource downButtonAudio;
    bool jump = false;
    bool slide = false;
    double jumptime = 1.0;
    void Start()
    {
        curHealth = playerStats.getCurHeath();
        maxHealth = playerStats.getTotalHealth();
        
        facingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jump = false;
        slide = false;
        wallcling = wallClingBox.iswallcling;
        choPhepDiChuyen = true;
    }
    void Update()
    {
        curHealth = playerStats.getCurHeath();
        maxHealth = playerStats.getTotalHealth();
        wallcling = wallClingBox.iswallcling;
        if (wallcling && !grounded)
        {
            setWallClinging();
            FreezeGravity();
            jump = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (choPhepDiChuyen == false) return;
        if (GetComponent<playerUseSkill>().isAtk) // Không thể vừa đánh vừa di chuyển
        {
            HandleMovement(0f);
        }
        else
        {
            GetInput();
            float horizontal = Input.GetAxis("Horizontal");
            HandleMovement(horizontal);
        }
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
        if (grounded) myAnimator.SetBool("move", false);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            runAudio.Play();
            myAnimator.SetBool("move", true);
            
            
            if (wallcling && facingRight)
            {
                wallClingBox.iswallcling = false;
                UnfreezeGravity();
                setJump();
            }
            direction += Vector2.left;
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
            if (wallcling && !facingRight)
            {
                wallClingBox.iswallcling = false;
                UnfreezeGravity();
                setJump();
            }
            direction += Vector2.right;
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
            UnfreezeGravity();
            jumpAudio.Play();
            myAnimator.SetBool("move", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            jump = true;
            wallClingBox.iswallcling = false;
            grounded = false;
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
            wallClingBox.iswallcling = false;
            wallcling = false;
            grounded = true;
            jump = false;
       }
        if (other.gameObject.tag == "checkpoint")
        {
            missionControl.GetComponent<MisionLoader>().setLastHP(curHealth);
        }
    }
    public void ReceivesDamage(float damage)
    {
        curHealth -= damage;
        playerStats.UpdateHealth(curHealth);
        missionControl.GetComponent<MisionLoader>().checkDropHP(curHealth);
    }
    private void FreezeGravity()
    {
        myRigidbody2D.gravityScale = 0;
    }
    private void UnfreezeGravity()
    {
        myRigidbody2D.gravityScale = gravityScaleValue;
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