using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeControl : MonoBehaviour
{
    public GameObject noticeObj;
    public GameObject btnNotice;
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
        if (Input.GetKeyDown(KeyCode.F5) && Input.GetKey(KeyCode.F5))
        {
            btnNotice.SetActive(status);
            status = !status;
            noticeObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        btnNotice.SetActive(status);
        status = false;
        noticeObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        btnNotice.SetActive(status);
        status = !status;
        noticeObj.SetActive(status);
    }
}
