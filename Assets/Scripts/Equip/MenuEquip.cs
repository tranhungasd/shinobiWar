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
    public Image[] shortcutImage = new Image[5];
    public TextMeshProUGUI tmpTxt;
    private string input;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    // public TextMeshProUGUI[] itemS = new TextMeshProUGUI[6];
    private GameObject currentKey;
    public TextMeshProUGUI txtBtn;

    public string[] txtBtnShortcut = new string[32];

    public ReadParameterItem parItem;

    public class tagParameter
    {
        public string[] id = new string[35];
        public string[] name = new string[35];
        public int count;
    }
    public tagParameter line = new tagParameter();
    public class tagShortcut
    {
        public string[] hotkey = new string[5];
        public int[] id = new int[5];
        public int count;
    }
    public tagShortcut shortcutItem = new tagShortcut();
    public TextMeshProUGUI tmpShortcut;
    private string inputShortcut;
    public TextMeshProUGUI noticeMain;


    public ParameterPlayer paraPlayer;
    public ReadProfile profilePlayer;

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
    public void HotkeyUse(int _idItem)
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
        int item = 0;
        for (int i = 0; i < 34; i++)
        {
            if (ed.line.id[i] == _idItem)
            {
                item = ed.line.item[i];
                //Debug.Log(item + "  " + ed.line.item[i] + " " + getQuantity(item).ToString());
                break;
            }
        }
        ed.Change(item, _idItem, getQuantity(item) - 1);
        ed.ReadAll();
        UseItem(_idItem);
    }
    public void Use()
    {
        int item = PlayerPrefs.GetInt("item");
        int idItem = getIDItem(item);
        int quantityItem = getQuantity(item);
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.Change(item, idItem, quantityItem - 1);
        ed.ReadAll();
        UseItem(idItem);
    }
    public void UseItem(int _id)
    {
        //paraPlayer = paraPlayer.GetComponent<ParameterPlayer>();
        //Debug.Log(paraPlayer);
        paraPlayer.ReadAll();
        if (paraPlayer == null)
        {
            return;
        }
        for (int i = 1; i < 5; i++)
        {
            for (int j = 0; j <= 5; j++)
            {

                if (_id == 4 * j + i)
                {
                    if (i == 1)
                    {
                        paraPlayer.AddDamage("DMG0", valueItem(_id) * 10);
                        paraPlayer.AddDamage("DMG1", valueItem(_id) * 10 + 10);
                        paraPlayer.AddDamage("DMG3", valueItem(_id) * 10 + 15);
                        paraPlayer.AddDamage("DMG5", valueItem(_id) * 10 + 20);
                    }
                    else if (i == 2)
                    {
                        paraPlayer.HPUP("HP", valueItem(_id) * 800);

                    }
                    else if (i == 3)
                    {
                        paraPlayer.HPUP("HP", valueItem(_id) * 300);

                    }
                    else
                    {
                        paraPlayer.HPUP("HP", valueItem(_id) * 100);
                    }
                    profilePlayer.ChangeProfile(i - 1, _id);
                }
            }
        }
        if (_id == 26)
        {
            paraPlayer.AddHP("HP", valueItem(_id) * 1000);
        }
        else if (_id == 25)
        {
            paraPlayer.AddExp(valueItem(_id) * 8000);
        }
        else
        {
            paraPlayer.AddDamage("DMG0", valueItem(_id) * 100);
            paraPlayer.AddDamage("DMG1", valueItem(_id) * 100 + 10);
            paraPlayer.AddDamage("DMG3", valueItem(_id) * 100 + 15);
            paraPlayer.AddDamage("DMG5", valueItem(_id) * 100 + 20);
            paraPlayer.HPUP("HP", valueItem(_id) * 1000);
        }
        //Debug.Log(valueItem(_id).ToString());
        noticeMain.SetText(noticeMain.text + "- Used " + line.name[_id] + "\n");
    }
    public float valueItem(int _id)
    {
        return parItem.parameterItem.value[_id];
    }
    public void Delete()
    {
        int item = PlayerPrefs.GetInt("item");
        int idItem = getIDItem(item);
        int quantityItem = getQuantity(item);
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.Change(item, idItem, quantityItem - 1);
        ed.ReadAll();
    }
    public void checkItem()
    {
        EquipDatabase ed = GetComponent<EquipDatabase>();
        ed.ReadAll();
        int item = PlayerPrefs.GetInt("item");
        int idItem = getIDItem(item);
        int quantityItem = getQuantity(item);
        if (idItem == 0 || quantityItem == 0)
        {
            Debug.Log(item.ToString());
            Menu.SetActive(false);
            return;
        }
        id = ed.line.id[item];
        quantity = ed.line.quantity[item];
        tmpDes.SetText(line.name[id] + "\nQuantity: " + quantity.ToString());
        if (txtBtnShortcut[idItem] == "")
        {
            txtBtn.SetText("");
        }
        else
        {
            txtBtn.SetText(txtBtnShortcut[idItem]);
        }
        oldImage = objItem.GetComponent<Image>();
        //Debug.Log(quantity.ToString());
        newImage = imageItem[id].GetComponent<Image>();
        //Debug.Log(newImage);
        oldImage.sprite = newImage.sprite;
    }
    private int getIDItem(int _item)
    {
        Menu.GetComponent<EquipDatabase>().ReadAll();
        return Menu.GetComponent<EquipDatabase>().line.id[_item];
    }
    private int getQuantity(int _item)
    {
        Menu.GetComponent<EquipDatabase>().ReadAll();
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
    void OnGUI()
    {
        int item = PlayerPrefs.GetInt("item");
        int idItem = getIDItem(item);
        int quantityItem = getQuantity(item);
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey = null;
            }
            if (e.keyCode.ToString() == "Alpha1")
            {
                for (int i = 0; i < 32; i++)
                {
                    if (e.keyCode.ToString() == txtBtnShortcut[i])
                    {
                        txtBtnShortcut[i] = "";
                        break;
                    }
                }
                newImage = imageItem[id].GetComponent<Image>();
                shortcutImage[0].sprite = newImage.sprite;
                txtBtnShortcut[idItem] = e.keyCode.ToString();
                shortcutItem.id[0] = idItem;
                reWriteShortcut();
                //Debug.Log(idItem.ToString());
            }
            else if (e.keyCode.ToString() == "Alpha2")
            {
                for (int i = 0; i < 32; i++)
                {
                    if (e.keyCode.ToString() == txtBtnShortcut[i])
                    {
                        txtBtnShortcut[i] = "";
                        break;
                    }
                }
                newImage = imageItem[id].GetComponent<Image>();
                shortcutImage[1].sprite = newImage.sprite;
                txtBtnShortcut[idItem] = e.keyCode.ToString();
                shortcutItem.id[1] = idItem;
                reWriteShortcut();
            }
            else if (e.keyCode.ToString() == "Alpha3")
            {
                for (int i = 0; i < 32; i++)
                {
                    if (e.keyCode.ToString() == txtBtnShortcut[i])
                    {
                        txtBtnShortcut[i] = "";
                        break;
                    }
                }
                newImage = imageItem[id].GetComponent<Image>();
                shortcutImage[2].sprite = newImage.sprite;
                txtBtnShortcut[idItem] = e.keyCode.ToString();
                shortcutItem.id[2] = idItem;
                reWriteShortcut();
            }
            else if (e.keyCode.ToString() == "Alpha4")
            {
                for (int i = 0; i < 32; i++)
                {
                    if (e.keyCode.ToString() == txtBtnShortcut[i])
                    {
                        txtBtnShortcut[i] = "";
                        break;
                    }
                }
                newImage = imageItem[id].GetComponent<Image>();
                shortcutImage[3].sprite = newImage.sprite;
                txtBtnShortcut[idItem] = e.keyCode.ToString();
                shortcutItem.id[3] = idItem;
                reWriteShortcut();
            }
            else if (e.keyCode.ToString() == "Alpha5")
            {
                for (int i = 0; i < 32; i++)
                {
                    if (e.keyCode.ToString() == txtBtnShortcut[i])
                    {
                        txtBtnShortcut[i] = "";
                        break;
                    }
                }
                newImage = imageItem[id].GetComponent<Image>();
                shortcutImage[4].sprite = newImage.sprite;
                txtBtnShortcut[idItem] = e.keyCode.ToString();
                shortcutItem.id[4] = idItem;
                reWriteShortcut();
            }
        }
    }
    public void changeKey(GameObject clicked)
    {
        currentKey = clicked;
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
    private void reWriteShortcut()
    {
        tmpShortcut.text = "";
        for (int i = 0; i < 5; i++)
        {
            tmpShortcut.text += "Alpha" + (i + 1).ToString() + "/" + shortcutItem.id[i] + "\n";
        }
        ReadShortcutItem();
    }
    public GameObject[] GetImageItem()
    {
        GameObject[] _imageItem = new GameObject[35];
        _imageItem = imageItem;
        return _imageItem;
    }
}
