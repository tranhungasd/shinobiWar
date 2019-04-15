using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCamera : MonoBehaviour
{
    public Transform Player;
    private void FixedUpdate()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
    }
}
