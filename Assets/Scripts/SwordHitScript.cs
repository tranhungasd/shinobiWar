using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitScript : MonoBehaviour
{
    public bool isHitting;
    private int damage;
    public BoxCollider2D swordbox;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        damage = transform.parent.gameObject.GetComponent<MainPlayer>().damage;
        swordbox.enabled = false;
        isHitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(damage);
        //if (enemy != null)
        //{
        //    GameObject objPlayer = GameObject.FindWithTag("MainPlayer");
        //    Vector3 thePosEnemy = enemy.GetComponent<Rigidbody2D>().transform.localPosition;
        //    Vector3 theScale = objPlayer.GetComponent<Rigidbody2D>().transform.localScale;
        //    thePosEnemy.x = thePosEnemy.x + 0.1f * theScale.x;
        //    enemy.GetComponent<Rigidbody2D>().transform.localPosition = thePosEnemy;
        //}
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if ((other.gameObject.tag == "enemy") && (isHitting))
        {
            //Debug.Log(isHitting);
            Debug.Log("hit");
            // Đẩy lùi enemy
            enemy = other.gameObject;
            GameObject objPlayer = GameObject.FindWithTag("MainPlayer");
            Vector3 thePosEnemy = enemy.GetComponent<Rigidbody2D>().transform.localPosition;
            Vector3 theScale = objPlayer.GetComponent<Rigidbody2D>().transform.localScale;
            thePosEnemy.x = thePosEnemy.x + 2f * theScale.x;
            enemy.GetComponent<Rigidbody2D>().transform.localPosition = thePosEnemy;
            ///////////////////////
            other.gameObject.GetComponent<EnemyScipt>().ReceivesDamage(damage);
            isHitting = false;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.forward* 500);
        }
    }
}
