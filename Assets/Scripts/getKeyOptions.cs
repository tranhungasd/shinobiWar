using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class getKeyOptions : MonoBehaviour
{
    public TextMeshProUGUI txtBtn;
    private TextMeshProUGUI item;
    void Start()
    {
        item = GetComponent<TextMeshProUGUI>();
        item.text = formatText(txtBtn.text);
    }

    // Update is called once per frame
    void Update()
    {
        item = GetComponent<TextMeshProUGUI>();
        item.text = formatText(txtBtn.text);
    }
    private string formatText(string txt)
    {
        if (txt.Length > 3)
        {
            return txt[txt.Length - 1].ToString();
        }
        return txt;
    }
}
