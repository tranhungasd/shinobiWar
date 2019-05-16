using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class EquipDatabase : MonoBehaviour
{
    public TextMeshProUGUI tmpTxt;
    public string input;
    public class tagParameter
    {
        public int[] item = new int[35];
        public int[] id = new int[35];
        public int[] quantity = new int[35];
        public int count;
    }
    public tagParameter line = new tagParameter();
    void Start()
    {
        ReadAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadAll()
    {
        input = tmpTxt.text;
        string pattern = "\n";
        int i = 0;
        string[] elements = Regex.Split(input, pattern);
        foreach (string m in elements)
        {
            if (m != "")
            {
                line.count = i;
                string pat = "/";
                string[] tags = Regex.Split(m, pat);
                line.item[i] = int.Parse(TextFollowing(tags[0], "ITEM"));
                line.id[i] = int.Parse(tags[1]);
                line.quantity[i] = int.Parse(tags[2]);
                //Debug.Log(line.id[i]);
                Debug.Log(line.count + "-" + line.item[i] + "-" + line.id[i].ToString() + "-" + line.quantity[i].ToString() + "\n");
                i++;
            }
        }
        SortItem();
    }
    public void reRead()
    {
        string pattern = "\n";
        int i = 0;
        string[] elements = Regex.Split(input, pattern);
        foreach (string m in elements)
        {
            if (m != "")
            {
                line.count = i;
                string pat = "/";
                string[] tags = Regex.Split(m, pat);
                line.item[i] = int.Parse(TextFollowing(tags[0], "ITEM"));
                line.id[i] = int.Parse(tags[1]);
                line.quantity[i] = int.Parse(tags[2]);
                //Debug.Log(line.id[i]);
                //Debug.Log(line.count + "-" + line.item[i] + "-" + line.id[i].ToString() + "-" + line.quantity[i].ToString() + "\n");
                i++;
            }
        }
    }
    public void SortItem()
    {
        int isItem = 0;
        int[] isQuantity = new int[35];
        int[] isID = new int[35];
        int countItem = 32;
        for (int i = 0; i < countItem; i++)
        {
            if (line.id[i] != 0 && line.quantity[i] !=0)
            {
                isQuantity[isItem] = line.quantity[i];
                isID[isItem] = line.id[i];
                isItem++;
            }
        }
        for (int i = 0; i < countItem; i++)
        {
            line.id[i] = 0;
            line.quantity[i] = 0;
        }
        for (int i = 0; i <= isItem; i++)
        {
            line.id[i] = isID[i];
            line.quantity[i] = isQuantity[i];
        }
        for (int i = 0; i < isItem; i++)
        {
            for (int j = i + 1; j <= isItem; j++)
            {
                if (line.id[i] == line.id[j])
                {
                    line.quantity[i] += line.quantity[j];
                    line.id[j] = 0;
                    line.quantity[j] = 0;
                }
            }
        }
        Change(line);
    }
    public bool isFull()
    {
        if (line.count < 31)
        {
            return false;
        }
        return true;
    }
    public void ChangeGold(int _gold)
    {
        line.id[32] += _gold;
        Change(line);
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
    public void Change(tagParameter ED)
    {
        //tmpTxt.text = "";
        input = "";
        for (int i = 0; i <= ED.count; i++)
        {
            //Debug.Log("ITEM " + ED.id[i].ToString());
            line.item[i] = ED.item[i];
            line.id[i] = ED.id[i];
            line.quantity[i] = ED.quantity[i];
            input += "ITEM" + ED.item[i].ToString() + "/" + ED.id[i].ToString() + "/" + ED.quantity[i].ToString() + "\n";
        }
        tmpTxt.text = input;
        //Debug.Log(txtDB);
        //ReadAll();
        reRead();
    }
}
