using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenganScript : MonoBehaviour
{
    public int damage;
    private Animator myAnimator;
    private float existtime = 2;
    private GameObject objPlayer;
    private GameObject[] enemy = new GameObject[10];
    private int countEnemy = 0;
    private bool checkStop = false;
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
        objPlayer = GameObject.FindWithTag("MainPlayer");
        for (int i = 0; i < 10; i++)
        {
            if (enemy[i] != null)
            {
                Vector3 thePosSpell = GetComponent<Rigidbody2D>().transform.localPosition;
                enemy[i].GetComponent<Rigidbody2D>().transform.localPosition = thePosSpell;
            }
        }
        checkStop = objPlayer.GetComponent<playerUseSkill>().stopRasengan;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            // pos enemy = pos rasengan
            enemy[countEnemy] = col.gameObject;
            countEnemy++;
            //calls ReceivesDamage() in enemy script
            // resengan chạy thẳng gom enemy, khi stopRasengan = true thì mới tính damage và nổ tung;
            if (checkStop) 
            {
                StartCoroutine(EndSkill(col)); // effect end skill
            }
        }
    }
    IEnumerator EndSkill(Collider2D col)
    {
        myAnimator.SetBool("next", true); //Next State animator fire ball
        col.gameObject.GetComponent<EnemyScipt>().ReceivesDamage(damage);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
