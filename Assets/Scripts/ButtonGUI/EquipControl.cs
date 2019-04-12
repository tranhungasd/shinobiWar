using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipControl : MonoBehaviour
{
    public GameObject equipObj;
    private bool status = false;
    public AudioSource clickAudio;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F2) && Input.GetKey(KeyCode.F2))
        {
            clickAudio.Play();
            status = !status;
            equipObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        clickAudio.Play();
        status = false;
        equipObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        clickAudio.Play();
        status = !status;
        equipObj.SetActive(status);
    }
}
