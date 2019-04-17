using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScipt : MonoBehaviour
{
    private Animator myAnimator;
    private int curHealth;
    public int speed;
    public int damage;
    public int maxHealth;
    private bool grounded = false;
    private GameObject playerFinder;
    private Vector3 initialPosition;
    public float maxDist;
    public float minDist;
    private float originalPos;
    int direction;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        playerFinder = GameObject.Find("MainPlayer");
        myAnimator = GetComponent<Animator>();
        initialPosition = transform.position;
        //direction -1 is facing left, 1 is right
        direction = 1;
        originalPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth == 0 || curHealth < 0)
        {
            Destroy(this.gameObject);
        }
        if (grounded)
        {
            Move();
        }
    }
    private void Move()
    {
        setRun();
        //enemy auto moves left/right within max distance and min distance
        switch (direction)
        {
            case -1:
                // Moving Left    
                if ((transform.position.x - originalPos) > minDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    //flip
                    direction = 1;
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                break;
            case 1:
                //Moving Right
                if ((transform.position.x - originalPos) < maxDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    //flip
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                    direction = -1;
                }
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }
    public void ReceivesDamage(int damage)
    {
        Debug.Log(damage);
        curHealth -= damage;
    }
    private void setIdle()
    {
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("idle", 1);
        myAnimator.SetFloat("attack", 0);
    }
    private void setRun()
    {
        myAnimator.SetFloat("run", 1);
        myAnimator.SetFloat("idle", 0);
        myAnimator.SetFloat("attack", 0);
    }
}
