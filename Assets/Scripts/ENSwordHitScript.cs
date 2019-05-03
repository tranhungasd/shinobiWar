using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENSwordHitScript : MonoBehaviour
{
    public bool isHitting;
    private int damage;
    public BoxCollider2D ENswordbox;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        damage = transform.parent.gameObject.GetComponent<EnemyScipt>().damage;
        ENswordbox.enabled = false;
        isHitting = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(other.gameObject.tag);

        if ((other.gameObject.tag == "MainPlayer") && (isHitting))
        {
            other.gameObject.GetComponent<MainPlayer>().ReceivesDamage(damage);
            isHitting = false;
            // Đẩy lùi player
            player = other.gameObject;
            GameObject objEnemy = transform.parent.gameObject;
            Vector3 thePosPlayer = player.GetComponent<Rigidbody2D>().transform.localPosition;
            Vector3 theScale = objEnemy.GetComponent<Rigidbody2D>().transform.localScale;
            thePosPlayer.x = thePosPlayer.x + theScale.x;
            player.GetComponent<Rigidbody2D>().transform.localPosition = thePosPlayer;
            
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.forward * 50);
        }
    }
}
