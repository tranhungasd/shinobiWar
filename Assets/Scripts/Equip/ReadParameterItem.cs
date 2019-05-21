using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ReadParameterItem : MonoBehaviour
{
    public TextMeshProUGUI tmpParameterItem;
    private string input;
    public class tagParameterItem
    {
        public int[] id = new int[35];
        public string[] name = new string[35];
        public float[] value = new float[35];
        public int count;
    }
    public tagParameterItem parameterItem = new tagParameterItem();
    void Start()
    {
        ReadAll();
    }

    // Update is called once per frame
    void Update()
    {
        ReadAll();
    }

    private void ReadAll()
    {
        input = tmpParameterItem.text;
        string pattern = "\n";
        int i = 0;
        string[] elements = Regex.Split(input, pattern);
        foreach (string m in elements)
        {
            if (m != "")
            {
                parameterItem.count = i;
                string pat = "/";
                string[] tags = Regex.Split(m, pat);
                parameterItem.id[i] = int.Parse(TextFollowing(tags[0], "ITEM"));
                parameterItem.name[i] = tags[1];
                parameterItem.value[i] = float.Parse(tags[2]);
                //Debug.Log(line.count + " --- " + line.id[i] + "   ---   " + line.name[i]);
                i++;
            }
        }
    }

    public static string TextFollowing(string searchTxt, string value)
    {
        if (!String.IsNullOrEmpty(searchTxt) && !String.IsNullOrEmpty(value))
        {
            int index = searchTxt.IndexOf(value);
            if (-1 < index)
            {
                int start = index + value.Length;
                if (start <= searchTxt.Length)
                {
                    return searchTxt.Substring(start);
                }
            }
        }
        return null;
    }
}
