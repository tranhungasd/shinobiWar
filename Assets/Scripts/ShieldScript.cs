using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShieldScript : MonoBehaviour
{
    public bool aquired;
    private float existtime = 5;
    private GameObject enemy;
    private GameObject player;
    private GameObject shieldPoint;
    // Start is called before the first frame update
    void Start()
    {
        shieldPoint = GameObject.Find("shieldpoint");
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

