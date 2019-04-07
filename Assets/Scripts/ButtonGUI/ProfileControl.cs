using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileControl : MonoBehaviour
{
    public GameObject profileObj;
    private bool status = false;
    void Start()
    {

    }
    void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F1) && Input.GetKey(KeyCode.F1))
        {
            status = !status;
            profileObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        status = false;
        profileObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        status = !status;
        profileObj.SetActive(status);
    }
}
