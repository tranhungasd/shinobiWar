using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TextMeshProUGUI[] item = new TextMeshProUGUI[10];
    private GameObject currentKey;
    void Start()
    {
        keys.Add("First Skill", KeyCode.Q);
        keys.Add("Second Skill", KeyCode.W);
        keys.Add("Third Skill", KeyCode.E);
        keys.Add("Fourth Skill", KeyCode.R);
        keys.Add("Fifth Skill", KeyCode.T);
        keys.Add("First Item", KeyCode.Alpha1);
        keys.Add("Second Item", KeyCode.Alpha2);
        keys.Add("Third Item", KeyCode.Alpha3);
        keys.Add("Fourth Item", KeyCode.Alpha4);
        keys.Add("Fifth Item", KeyCode.Alpha5);

        item[0].text = keys["First Skill"].ToString();
        item[1].text = keys["Second Skill"].ToString();
        item[2].text = keys["Third Skill"].ToString();
        item[3].text = keys["Fourth Skill"].ToString();
        item[4].text = keys["Fifth Skill"].ToString();
        item[5].text = keys["First Item"].ToString();
        item[6].text = keys["Second Item"].ToString();
        item[7].text = keys["Third Item"].ToString();
        item[8].text = keys["Fourth Item"].ToString();
        item[9].text = keys["Fifth Item"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }
    public void changeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
