using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // Load từng item, positem = line.item
    [SerializeField]
    private int posItem;
    private int id;
    private int quantity;
    [SerializeField]
    GameObject objItem;
    [SerializeField]
    private TextMeshProUGUI tmpQuantity;
    [SerializeField]
    private GameObject[] imageItem = new GameObject[35];
    private Image oldImage;
    private Image newImage;
    EquipDatabase ed;
    void Start()
    {
        ed = GetComponent<EquipDatabase>();
        UpdataItem(posItem);
    }

    // Update is called once per frame
    void Update()
    {
        UpdataItem(posItem);
    }
    private void UpdataItem(int _id)
    {
        ed = GetComponent<EquipDatabase>();
        if (ed.input == null || ed.input == "")
        {
            return;
        }
        ed.ReadAll(); // <-- thiếu mỗi nó @@ :v đỉnh kao
        IEUpdate(_id);
    }
    private void IEUpdate(int _id)
    {
        //hàm nào gọi cái này m, ở script khác ấy
        // k có :v
        id = ed.line.id[_id];
        quantity = ed.line.quantity[_id];
        //Debug.Log(_id + " " + quantity);
        if (quantity == 0)
        {
            ed.line.id[_id] = 0;
            tmpQuantity.SetText("");
            ed.Change(ed.line.item[_id], 0, 0); //k có vật phẩm
        }
        else
        {
            tmpQuantity.SetText(quantity.ToString()); //ghi số lượng vp
        }
        oldImage = objItem.GetComponent<Image>();
        //Debug.Log(quantity.ToString());
        newImage = imageItem[id].GetComponent<Image>();
        //Debug.Log(newImage);
        oldImage.sprite = newImage.sprite;
    }
}
