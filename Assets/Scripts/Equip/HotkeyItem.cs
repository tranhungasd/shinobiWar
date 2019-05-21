using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class HotkeyItem : MonoBehaviour
{
    public TextMeshProUGUI tmpShortcut;
    private string inputShortcut;
    public MenuEquip ME;
    public class tagShortcut
    {
        public string[] hotkey = new string[5];
        public int[] id = new int[5];
        public int count;
    }
    public tagShortcut shortcutItem = new tagShortcut();
    void Start()
    {
        ReadShortcutItem();
    }

    // Update is called once per frame
    void Update()
    {
        ReadShortcutItem();
        GetInput();
    }
    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha1))
        {
            //Debug.Log("item " + shortcutItem.id[0].ToString());
            ME.HotkeyUse(shortcutItem.id[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.Alpha2))
        {
            //Debug.Log("item " + shortcutItem.id[1].ToString());
            ME.HotkeyUse(shortcutItem.id[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.Alpha3))
        {
            //Debug.Log("item " + shortcutItem.id[2].ToString());
            ME.HotkeyUse(shortcutItem.id[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Input.GetKey(KeyCode.Alpha4))
        {
            //Debug.Log("item " + shortcutItem.id[3].ToString());
            ME.HotkeyUse(shortcutItem.id[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && Input.GetKey(KeyCode.Alpha5))
        {
            //Debug.Log("item " + shortcutItem.id[4].ToString());
            ME.HotkeyUse(shortcutItem.id[4]);
        }
    }
    private void ReadShortcutItem()
    {
        inputShortcut = tmpShortcut.text;
        string pattern = "\n";
        int i = 0;
        string[] elements = Regex.Split(inputShortcut, pattern);
        foreach (string m in elements)
        {
            if (m != "")
            {
                shortcutItem.count = i;
                string pat = "/";
                string[] tags = Regex.Split(m, pat);
                shortcutItem.hotkey[i] = tags[0];
                shortcutItem.id[i] = int.Parse(tags[1]);
                //Debug.Log(line.count + " --- " + line.id[i] + "   ---   " + line.name[i]);
                i++;
            }
        }
    }
}
