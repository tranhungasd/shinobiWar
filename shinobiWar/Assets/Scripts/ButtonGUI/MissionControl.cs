using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    public GameObject missionObj;
    private bool status = false;
    public AudioSource clickAudio;
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
            clickAudio.Play();
            status = !status;
            missionObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        clickAudio.Play();
        status = false;
        missionObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        clickAudio.Play();
        status = !status;
        missionObj.SetActive(status);
    }
}
