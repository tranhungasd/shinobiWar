using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScipt : MonoBehaviour
{
    private int curHealth;
    public int speed;
    public int damage;
    public int maxHealth;
    private GameObject playerFinder;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        playerFinder = GameObject.Find("MainPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if(curHealth == 0 || curHealth < 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void ReceivesDamage(int damage)
    {
        curHealth -= damage;
    }
}
