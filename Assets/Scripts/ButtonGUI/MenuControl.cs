using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject menuObj;
    //public GameObject quitObj;
    // Start is called before the first frame update
    private bool statusObj = false; //False = Hide, True = Show 
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
            statusObj = !statusObj;
            menuObj.SetActive(statusObj);
            //Debug.Log(statusObj);
        }
    }
    public void quitClick(GameObject gO)
    {
        statusObj = false;
        menuObj.SetActive(statusObj);
    }
    public void escClick(GameObject gO)
    {
        statusObj = !statusObj;
        menuObj.SetActive(statusObj);
    }
}
