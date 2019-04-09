using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUseSkill : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private SpriteRenderer sp;
    private bool isAtk = false;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.Space) && !isAttack())
        {
            startAttack();
            StartCoroutine(AttackNormal());
        }
        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.Q) && !isAttack())
        {
            startAttack();
            StartCoroutine(Attack1());
        }
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.E) && !isAttack())
        {
            startAttack();
            StartCoroutine(Attack3());
        }
    }
    private IEnumerator AttackNormal()
    {

        sword();
        Debug.Log(sp.sprite.name.ToString());
        yield return new WaitForSeconds(0.7f);
        stopAttack();
    }
    private IEnumerator Attack1()
    {
        shuriken();
        yield return new WaitForSeconds(0.7f);
        Debug.Log("attack");
        stopAttack();
    }
    private IEnumerator Attack3()
    {
        fire();
        yield return new WaitForSeconds(0.7f);
        Debug.Log("attack");
        stopAttack();
    }
    private bool isAttack()
    {
        if (isAtk)
        {
            Debug.Log("wait...");
            return !isAtk;
        }
        isAtk = !isAtk;
        myAnimator.SetBool("attack", isAtk);
        return isAtk;
    }
    private void startAttack()
    {
        resetAttack();
        ActivateLayer("AttackPlayer");
        myAnimator.SetBool("attack", true);
    }
    private void stopAttack()
    {
        myAnimator.SetBool("attack", false);
        ActivateLayer("MovePlayer");
    } 
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }
    private void resetAttack()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void sword()
    {
        myAnimator.SetFloat("sword", 1);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void fire()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 1);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void shuriken()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 1);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
}
