using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class GiftCode : MonoBehaviour
{
    public GameObject TableGiftCode;
    public TextMeshProUGUI tmpTxt;
    public TextMeshProUGUI txtED;
    public TextMeshProUGUI noticeMain;
    private string input;
    public TextMeshProUGUI tmpNameItem;
    public class tagParameter
    {
        public string[] id = new string[35];
        public string[] name = new string[35];
        public int count;
    }
    public tagParameter line = new tagParameter();
    // Start is called before the first frame update
    void Start()
    {
        ReadNameItem();
        //ed = GetComponent<EquipDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        //ed = GetComponent<EquipDatabase>();
    }
    public void ClickGiftCode()
    {
        if (TableGiftCode.activeSelf)
        {
            TableGiftCode.SetActive(false);
            return;
        }
        TableGiftCode.SetActive(true);
    }
    public void ButtonEnter()
    {
        //Debug.Log(tmpTxt.text);
        try
        {
            ReceiveItem(int.Parse(tmpTxt.text));
            tmpTxt.SetText("");
            ClickGiftCode();
        }
        catch (System.Exception)
        {
            return;
        }
    }
    public void ReceiveItem(int _giftCode)
    {
        if (_giftCode == 1010)
        {
            ReceiveAllSword();
            return;
        }
        if (_giftCode == 2020)
        {
            ReceiveAllClothes();
            return;
        }
        if (_giftCode == 3030)
        {
            ReceiveAllHat();
            return;
        }
        if (_giftCode == 4040)
        {
            ReceiveAllShoe();
            return;
        }
        if (_giftCode == 9999)
        {
            ReceiveSupport();
            return;
        }
        noticeMain.SetText(noticeMain.text + "- Error!\n");
    }
    public void ReceiveAllSword()
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.ReadAll();
        //ed.input = txtED.text;
        //ed.ReadAllNew(ed.input);
        int sword = -3;
        for (int i = 0; i < ed.line.count; i++)
        {
            if (ed.line.id[i] == 0)
            {
                sword = sword + 4;
                ed.line.id[i] = sword;
                ed.line.quantity[i] = ed.line.quantity[i] + 1;
                ed.Change(i, sword, ed.line.quantity[i]);
                noticeMain.SetText(noticeMain.text + "- Receive " + line.name[sword] + "!\n");
                //Debug.Log(ed.line.id[i].ToString() +" " + ed.line.quantity[i]);

            }
            if (sword > 20)
            {
                break;
            }
        }
        //Debug.Log("tang len .");
    }
    public void ReceiveAllClothes()
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.ReadAll();
        //ed.input = txtED.text;
        //ed.ReadAllNew(ed.input);
        int clothes = -2;
        for (int i = 0; i < ed.line.count; i++)
        {
            if (ed.line.id[i] == 0)
            {
                clothes = clothes + 4;
                ed.line.id[i] = clothes;
                ed.line.quantity[i] = ed.line.quantity[i] + 1;
                ed.Change(i, clothes, ed.line.quantity[i]);
                noticeMain.SetText(noticeMain.text + "- Receive " + line.name[clothes] + "!\n");
                //Debug.Log(ed.line.id[i].ToString() +" " + ed.line.quantity[i]);

            }
            if (clothes > 21)
            {
                break;
            }
        }
    }
    public void ReceiveAllHat()
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.ReadAll();
        //ed.input = txtED.text;
        //ed.ReadAllNew(ed.input);
        int hat = -1;
        for (int i = 0; i < ed.line.count; i++)
        {
            if (ed.line.id[i] == 0)
            {
                hat = hat + 4;
                ed.line.id[i] = hat;
                ed.line.quantity[i] = ed.line.quantity[i] + 1;
                ed.Change(i, hat, ed.line.quantity[i]);
                noticeMain.SetText(noticeMain.text + "- Receive " + line.name[hat] + "!\n");
                //Debug.Log(ed.line.id[i].ToString() +" " + ed.line.quantity[i]);

            }
            if (hat > 22)
            {
                break;
            }
        }
    }
    public void ReceiveAllShoe()
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.ReadAll();
        //ed.input = txtED.text;
        //ed.ReadAllNew(ed.input);
        int shoe = 0;
        for (int i = 0; i < ed.line.count; i++)
        {
            if (ed.line.id[i] == 0)
            {
                shoe = shoe + 4;
                ed.line.id[i] = shoe;
                ed.line.quantity[i] = ed.line.quantity[i] + 1;
                ed.Change(i, shoe, ed.line.quantity[i]);
                noticeMain.SetText(noticeMain.text + "- Receive " + line.name[shoe] + "!\n");
                //Debug.Log(ed.line.id[i].ToString() +" " + ed.line.quantity[i]);

            }
            if (shoe > 23)
            {
                break;
            }
        }
    }
    public void ReceiveSupport()
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.ReadAll();
        //ed.input = txtED.text;
        //ed.ReadAllNew(ed.input);
        for (int i = 0; i <  ed.line.count; i++)
        {
            if (ed.line.id[i] == 0)
            {
                ed.line.id[i] = i;
                ed.line.quantity[i] = ed.line.quantity[i] + 100;
                ed.Change(i, i, ed.line.quantity[i]);
                noticeMain.SetText(noticeMain.text + "- Receive " + line.name[i] + "!\n");
                //Debug.Log(ed.line.id[i].ToString() +" " + ed.line.quantity[i]);

            }
        }
        ed.line.quantity[32] += 1000000;
        ed.line.quantity[33] += 1000000;
        ed.Change(32, 32, ed.line.quantity[32]);
        ed.Change(33, 33, ed.line.quantity[33]);
        noticeMain.SetText(noticeMain.text + "- Receive 1,000,000 Gold!\n");
        noticeMain.SetText(noticeMain.text + "- Receive 1,000,000 Point!\n");
    }
    private void ReadNameItem()
    {
        input = tmpNameItem.text;
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
                line.id[i] = tags[0];
                line.name[i] = tags[1];
                //Debug.Log(line.count + " --- " + line.id[i] + "   ---   " + line.name[i]);
                i++;
            }
        }
    }
}
