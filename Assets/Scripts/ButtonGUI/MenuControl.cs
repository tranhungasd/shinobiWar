using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject menuObj;
    //public GameObject quitObj;
    // Start is called before the first frame update
    private bool statusObj = false; //False = Hide, True = Show 
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
        if (Input.GetKey(KeyCode.Escape) && Input.GetKeyDown(KeyCode.Escape))
        {
            clickAudio.Play();
            statusObj = !statusObj;
            menuObj.SetActive(statusObj);
            //Debug.Log(statusObj);
        }
    }
    public void quitClick(GameObject gO)
    {
        clickAudio.Play();
        statusObj = false;
        menuObj.SetActive(statusObj);
    }
    public void escClick(GameObject gO)
    {
        clickAudio.Play();
        statusObj = !statusObj;
        menuObj.SetActive(statusObj);
    }
}
