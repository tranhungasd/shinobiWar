using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesControl : MonoBehaviour
{
    public GameObject abilitiesObj;
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
        if (Input.GetKeyDown(KeyCode.F3) && Input.GetKey(KeyCode.F3))
        {
            clickAudio.Play();
            status = !status;
            abilitiesObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        clickAudio.Play();
        status = false;
        abilitiesObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        clickAudio.Play();
        status = !status;
        abilitiesObj.SetActive(status);
    }
}
