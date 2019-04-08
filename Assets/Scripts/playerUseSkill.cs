using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUseSkill : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private SpriteRenderer sp;
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
        if (Input.GetKeyDown(KeyCode.Space) && !myAnimator.GetBool("attack"))
        {
            startAttack();
            StartCoroutine(AttackNormal());
        }
        if (Input.GetKeyDown(KeyCode.Q) && !myAnimator.GetBool("attack"))
        {
            startAttack();
            StartCoroutine(Attack1());
        }
        if (Input.GetKeyDown(KeyCode.E) && !myAnimator.GetBool("attack"))
        {
            startAttack();
            StartCoroutine(Attack3());
        }
    }
    private IEnumerator AttackNormal()
    {

        sword();
        Debug.Log(sp.sprite.name.ToString());
        yield return new WaitForSeconds(0.5f);
        stopAttack();
    }
    private IEnumerator Attack1()
    {
        shuriken();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("attack");
        stopAttack();
    }
    private IEnumerator Attack3()
    {
        fire();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("attack");
        stopAttack();
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
        setIdle();
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
        myAnimator.SetFloat("rasengan", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void sword()
    {
        myAnimator.SetFloat("sword", 1);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void fire()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 1);
        myAnimator.SetFloat("rasengan", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void shuriken()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 1);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void setIdle()
    {
        myAnimator.SetFloat("cdn", 0);
        myAnimator.SetFloat("idle", 1);
        myAnimator.SetFloat("run", 0);
        myAnimator.SetFloat("jump", 0);
    }
    public IEnumerator SkillFire()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("rasengan", 0);
        myAnimator.SetFloat("resengan2", 0);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            myAnimator.SetFloat("fire", i / 10);
        }
    }
}
