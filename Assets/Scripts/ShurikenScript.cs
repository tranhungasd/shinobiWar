using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    public int damage;
    private float existtime = 2;

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
            col.gameObject.GetComponent<EnemyScipt>().ReceivesDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
