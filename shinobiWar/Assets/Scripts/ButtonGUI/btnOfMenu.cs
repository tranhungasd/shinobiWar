using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnOfMenu : MonoBehaviour
{
    public GameObject Obj;
    private bool status = false;
    public void OnClick()
    {
        status = !status;
        Obj.SetActive(status);
    }
}
