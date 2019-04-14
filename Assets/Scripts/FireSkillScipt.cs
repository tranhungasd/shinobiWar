using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkillScipt : MonoBehaviour
{
   
    public int damage;
    private Animator myAnimator;
    private float existtime = 2;
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
}
