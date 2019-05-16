using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilitesStats : MonoBehaviour
{
    private ParameterPlayer paraSkills;
    public static int items;
    public TextMeshProUGUI[] skilldes = new TextMeshProUGUI[items];
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<items; i++)
        {
            skilldes[i].text = "Damage: " + paraSkills.getDamage(("DMG"+i).ToString()).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}