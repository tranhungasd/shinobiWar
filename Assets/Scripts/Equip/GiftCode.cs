using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftCode : MonoBehaviour
{
    public GameObject TableGiftCode;
    public TextMeshProUGUI tmpTxt;
    public TextMeshProUGUI txtED;
    // Start is called before the first frame update
    void Start()
    {
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
        }
        catch (System.Exception)
        {
            return;
        }
    }
    public void ReceiveItem(int _giftCode)
    {
        if (_giftCode == 1)
        {
            ReceiveAllSword();
            return;
        }
        if (_giftCode == 2)
        {
            ReceiveAllClothes();
            return;
        }
        if (_giftCode == 3)
        {
            ReceiveAllHat();
            return;
        }
        if (_giftCode == 4)
        {
            ReceiveAllShoe();
            return;
        }
        if (_giftCode == 5)
        {
            ReceiveSupport();
            return;
        }
    }
    public void ReceiveAllSword()
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
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
                //Debug.Log(ed.line.id[i].ToString());
            }
            if (sword > 20)
            {
                break;
            }
        }
        ed.SortItem();
        //Debug.Log("tang len .");
    }
    public void ReceiveAllClothes()
    {

    }
    public void ReceiveAllHat()
    {

    }
    public void ReceiveAllShoe()
    {

    }
    public void ReceiveSupport()
    {

    }
}
