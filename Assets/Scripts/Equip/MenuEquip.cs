using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuEquip : MonoBehaviour
{
    public GameObject Menu;
    [SerializeField]
    public TextMeshProUGUI tmpDes;
    private int id;
    private int quantity;
    [SerializeField]
    GameObject objItem;
    [SerializeField]
    private GameObject[] imageItem = new GameObject[35];
    private Image oldImage;
    private Image newImage;
    public TextMeshProUGUI tmpTxt;
    private string input;
    public class tagParameter
    {
        public string[] id = new string[35];
        public string[] name = new string[35];
        public int count;
    }
    public tagParameter line = new tagParameter();
    private void Start()
    {
        ReadNameItem();
        checkItem();
    }
    private void Update()
    {
        checkItem();
    }
    public void ExitMenu()
    {
        Menu.SetActive(false);
    }
    public void Use()
    {
        int item = PlayerPrefs.GetInt("item");
        int idItem = getIDItem(item);
        int quantityItem = getQuantity(item);
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.line.quantity[item]--;
        ed.SortItem();

    }
    public void Delete()
    {

    }
    public void Setting()
    {

    }
    public void checkItem()
    {
        int item = PlayerPrefs.GetInt("item");
        int idItem = getIDItem(item);
        int quantityItem = getQuantity(item);
        if (idItem == 0 || quantityItem == 0)
        {
            Debug.Log(item.ToString());
            Menu.SetActive(false);
            return;
        }
        EquipDatabase ed = GetComponent<EquipDatabase>();
        id = ed.line.id[item];
        quantity = ed.line.quantity[item];
        tmpDes.SetText(line.name[id] + "\nQuantity: " + quantity.ToString());
        oldImage = objItem.GetComponent<Image>();
        //Debug.Log(quantity.ToString());
        newImage = imageItem[id].GetComponent<Image>();
        //Debug.Log(newImage);
        oldImage.sprite = newImage.sprite;
    }
    private int getIDItem(int _item)
    {
        return Menu.GetComponent<EquipDatabase>().line.id[_item];
    }
    private int getQuantity(int _item)
    {
        return Menu.GetComponent<EquipDatabase>().line.quantity[_item]; 
    }
    private void ReadNameItem()
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
                line.id[i] = tags[0];
                line.name[i] = tags[1];
                //Debug.Log(line.count + " --- " + line.id[i] + "   ---   " + line.name[i]);
                i++;
            }
        }
    }
}
