using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private Vector2 direction;
    [SerializeField]
    private float speed = 4;
    private bool facingRight;
    void Start()
    {
        facingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GetInput();
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
    }   
    private void HandleMovement(float horizontal)
    {
        Flip(horizontal);
        myRigidbody2D.velocity = new Vector2(horizontal * speed, myRigidbody2D.velocity.y);
        Debug.Log(horizontal);
        myAnimator.SetFloat("speed",Mathf.Abs(horizontal));
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }
    }
}
