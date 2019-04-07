using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUseSkill : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private Vector2 direction;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
