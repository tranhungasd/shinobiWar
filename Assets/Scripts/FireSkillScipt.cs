using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkillScipt : MonoBehaviour
{
   
    public float damage;
    private Animator myAnimator;
    private float existtime = 2;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        existtime -= Time.deltaTime;
        if (existtime < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            //calls ReceivesDamage() in enemy script
            // Đẩy lùi enemy
            Debug.Log(damage);
            enemy = col.gameObject;
            GameObject objPlayer = GameObject.FindWithTag("MainPlayer");
            Vector3 thePosEnemy = enemy.GetComponent<Rigidbody2D>().transform.localPosition;
            Vector3 theScale = objPlayer.GetComponent<Rigidbody2D>().transform.localScale;
            thePosEnemy.x = thePosEnemy.x + 2f * theScale.x;
            enemy.GetComponent<Rigidbody2D>().transform.localPosition = thePosEnemy;
            ///////////////////////
            StartCoroutine(EndSkill(col)); // effect end skill
        }
    }
    IEnumerator EndSkill(Collider2D col)
    {
        myAnimator.SetBool("next", true); //Next State animator fire ball
        col.gameObject.GetComponent<EnemyScipt>().ReceivesDamage(damage);
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
    public void UpdateDamage(float newDamage)
    {
        Debug.Log(damage);
        damage = newDamage;

    }
}
