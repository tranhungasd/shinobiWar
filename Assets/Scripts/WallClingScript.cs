using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool iswallcling;
    void Start()
    {
        iswallcling = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "wall"))
        {
            iswallcling = true;
        }
    }

}
