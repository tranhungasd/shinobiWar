using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShieldScript : MonoBehaviour
{
    private float existtime = 5;
    private GameObject enemy;
    private GameObject player;
    private GameObject shieldPoint;
    private GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        shieldPoint = GameObject.Find("shieldpoint");
        shield = GameObject.Find("Shield");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = shieldPoint.gameObject.transform.position;
        existtime -= Time.deltaTime;
        if (existtime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}

