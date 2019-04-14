using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitScript : MonoBehaviour
{
    public bool isHitting;
    public int damage;
    public Collider2D swordbox;
    // Start is called before the first frame update
    void Start()
    {
        isHitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);

        if ((other.gameObject.tag == "enemy") && (isHitting))
        {
            Debug.Log(isHitting);
            Debug.Log("hit");
            other.gameObject.GetComponent<EnemyScipt>().ReceivesDamage(damage);
            isHitting = false;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.forward* 500);
        }
    }
}
