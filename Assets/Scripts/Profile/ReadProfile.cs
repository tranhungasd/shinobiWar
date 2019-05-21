using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadProfile : MonoBehaviour
{
    public TextMeshProUGUI tmpProfile;
    public ParameterPlayer ParaPlayer;
    public MenuEquip MenuE;
    public TextMeshProUGUI tmpHP;
    public TextMeshProUGUI tmpEXP;
    public TextMeshProUGUI tmpDMG;
    public GameObject[] GOImageItem;
    public GameObject[] objItem;
    public class MyProfile
    {
        public int[] pos = new int[4];
        public int[] item = new int[4];
        public string hp;
        public string exp;
        public string damage;
        public int count;
    }
    public MyProfile myProfile;
    void Start()
    {
        ReadMyProfile();
    }

    // Update is called once per frame
    void Update()
    {
        ReadMyProfile();
    }
    public void ReadMyProfile()
    {
        myProfile = new MyProfile();
        string input = tmpProfile.text;
        string pattern = "\n";
        int i = 0;
        string[] elements = Regex.Split(input, pattern);
        foreach (string m in elements)
        {
            if (m != "")
            {
                myProfile.count = i;
                string pat = "/";
                string[] tags = Regex.Split(m, pat);
                myProfile.pos[i] = int.Parse(tags[0]);
                myProfile.item[i] = int.Parse(tags[1]);
                i++;
            }
        }
        //ParaPlayer.ReadAll();
        myProfile.hp = ParaPlayer.getCurHeath().ToString() + "/" + ParaPlayer.getTotalHealth().ToString();
        myProfile.exp = ParaPlayer.getCurExp().ToString() + "/" + ParaPlayer.getTotalExp().ToString();
        myProfile.damage = ParaPlayer.getDamage("DMG0").ToString();
        tmpHP.SetText(myProfile.hp);
        tmpEXP.SetText(myProfile.exp);
        tmpDMG.SetText(myProfile.damage);
        GOImageItem = MenuE.GetImageItem();
        //oldImage = objItem.GetComponent<Image>();
        //Debug.Log(quantity.ToString());
        //newImage = imageItem[id].GetComponent<Image>();
        //Debug.Log(newImage);
        for (int j = 0; j < 4; j++)
        {
            objItem[j].GetComponent<Image>().sprite = GOImageItem[myProfile.item[j]].GetComponent<Image>().sprite;
        }
    }
    public void ChangeProfile(int _pos, int _item)
    {
        myProfile.item[_pos] = _item;
        objItem[_pos].GetComponent<Image>().sprite = GOImageItem[myProfile.item[_pos]].GetComponent<Image>().sprite;
        tmpProfile.text = "";
        for (int i = 0; i < 4; i++)
        {
            tmpProfile.SetText(tmpProfile.text + i + "/" + myProfile.item[i] + "\n");
        }
    }
}
