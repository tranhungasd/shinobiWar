using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponent<EnemyScipt>();
    }

    // Update is called once per frame
    void Update()
    {
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
