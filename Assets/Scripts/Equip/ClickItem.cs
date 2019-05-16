using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
    public int item;
    public GameObject menu;
    public void OnClickItem()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            return;
        }
        item = int.Parse(GetComponent<Transform>().name);
        PlayerPrefs.SetInt("item", item);
        Debug.Log(item.ToString());
        menu.SetActive(true);
    }
}
