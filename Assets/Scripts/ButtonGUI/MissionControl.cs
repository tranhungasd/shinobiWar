using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    public GameObject missionObj;
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
        if (Input.GetKeyDown(KeyCode.F4) && Input.GetKey(KeyCode.F4))
        {
            status = !status;
            missionObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        status = false;
        missionObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        status = !status;
        missionObj.SetActive(status);
    }
}
