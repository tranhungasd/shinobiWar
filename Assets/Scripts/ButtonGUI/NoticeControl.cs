using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeControl : MonoBehaviour
{
    public GameObject noticeObj;
    public GameObject btnNotice;
    public AudioSource clickAudio;
    [SerializeField]
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
            clickAudio.Play();
            btnNotice.SetActive(status);
            status = !status;
            noticeObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        clickAudio.Play();
        btnNotice.SetActive(true);
        noticeObj.SetActive(false);
        status = false;
    }
    public void hotkeyClick(GameObject gO)
    {
        clickAudio.Play();
        btnNotice.SetActive(false);
        noticeObj.SetActive(true);
        status = true;
    }
}
