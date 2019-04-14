using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenganScript : MonoBehaviour
{
    public int damage;
    private Animator myAnimator;
    private float existtime = 2;
    private GameObject objPlayer;
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
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            // pos enemy = pos rasengan
            Vector3 thePosSpell = GetComponent<Rigidbody2D>().transform.localPosition;
            col.gameObject.GetComponent<Rigidbody2D>().transform.localPosition = thePosSpell;
            //calls ReceivesDamage() in enemy script
            // resengan chạy thẳng gom enemy, khi stopRasengan = true thì mới tính damage và nổ tung;
            bool check = objPlayer.GetComponent<playerUseSkill>().stopRasengan;
            Debug.Log(check.ToString());
            if (check) 
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
