using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    public float damage;
    private float existtime = 2;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {

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
            enemy = col.gameObject;
            GameObject objPlayer = GameObject.FindWithTag("MainPlayer");
            Vector3 thePosEnemy = enemy.GetComponent<Rigidbody2D>().transform.localPosition;
            Vector3 theScale = objPlayer.GetComponent<Rigidbody2D>().transform.localScale;
            thePosEnemy.x = thePosEnemy.x + 1.5f * theScale.x;
            enemy.GetComponent<Rigidbody2D>().transform.localPosition = thePosEnemy;
            ///////////////////////
            col.gameObject.GetComponent<EnemyScipt>().ReceivesDamage(damage);
            Destroy(this.gameObject);
        }
    }
    public void UpdateDamage(float newDamage)
    {
        Debug.Log(damage);
        damage = newDamage;
    }
}
