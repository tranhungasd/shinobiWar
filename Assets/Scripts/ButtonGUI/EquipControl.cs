using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipControl : MonoBehaviour
{
    public GameObject equipObj;
    private bool status = false;
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
            status = !status;
            equipObj.SetActive(status);
        }
    }
    public void quitClick(GameObject gO)
    {
        status = false;
        equipObj.SetActive(status);
    }
    public void hotkeyClick(GameObject gO)
    {
        status = !status;
        equipObj.SetActive(status);
    }
}
