using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesControl : MonoBehaviour
{
    public GameObject abilitiesObj;
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
        if (Input.GetKeyDown(KeyCode.F3) && Input.GetKey(KeyCode.F3))
        {
            status = !status;
            abilitiesObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        status = false;
        abilitiesObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        status = !status;
        abilitiesObj.SetActive(status);
    }
}
