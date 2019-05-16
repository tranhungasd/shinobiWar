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
    public tagParameter line;
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
        line = new tagParameter();
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
                //Debug.Log(line.item[i].ToString() + "  --  " + line.count.ToString());
                //Debug.Log(line.count + "-" + line.item[i] + "-" + line.id[i].ToString() + "-" + line.quantity[i].ToString() + "\n");
                i++;
            }
        }
        //SortItem(); bỏ sort q
    }
    public void SortItem()
    {
        int isItem = 0;
        tagParameter newLine = new tagParameter();
        int countItem = 32;
        line.item[32] = 32;
        line.item[33] = 33;
        for (int i = 0; i < countItem - 1; i++)
        {
            for (int j = i + 1; j < countItem; j++)
            {
                if (line.id[i] == line.id[j])
                {
                    line.quantity[i] = line.quantity[i] + line.quantity[j];
                    line.id[j] = 0;
                    line.quantity[j] = 0;
                }
            }
        }
        for (int i = 0; i < countItem; i++)
        {
            if (line.id[i] != 0 && line.quantity[i] != 0)
            {
                newLine.id[isItem] = line.id[i];
                newLine.quantity[isItem] = line.quantity[i];
                isItem++;
            }
        }
        tmpTxt.text = "";
        for (int i = 0; i < 34; i++)
        {
            line.item[i] = i;
            line.id[i] = newLine.id[i];
            line.quantity[i] = newLine.quantity[i];
            tmpTxt.SetText(tmpTxt.text + "ITEM" + i.ToString() + "/" + line.id[i].ToString() + "/" + line.quantity[i].ToString() + "\n");
            //Debug.Log("ITEM" + i.ToString());
        }        
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
    public void Change(int _item, int _id, int _quantity)
    {
        for (int i = 0; i <= line.count; i++)
        {
            if (line.item[i] == _item)
            {
                line.id[i] = _id;
                line.quantity[i] = _quantity;
                break;
            }
        }
        reWrite();
    }
    public void reWrite()
    {
        tmpTxt.text = "";
        for (int i = 0; i <= line.count; i++)
        {
            tmpTxt.SetText(tmpTxt.text + "ITEM" + i.ToString() + "/" + line.id[i].ToString() + "/" + line.quantity[i].ToString() + "\n");
        }
    }
}
