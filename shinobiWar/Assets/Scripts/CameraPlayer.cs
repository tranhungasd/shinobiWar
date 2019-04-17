using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset;
    public Camera cam;
    // Use this for initialization
    void Start()
    {
        offset = cam.transform.position - player.transform.position;
        offset.x = 0;
        offset.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = player.transform.position + offset;
    }

}
