using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    EquipDatabase ed;
    [SerializeField]
    private TextMeshProUGUI tmpGold;
    [SerializeField]
    private TextMeshProUGUI tmpPoint;
    void Start()
    {
        ed = GetComponent<EquipDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdateScore());
        ed.ReadAll();
    }
    IEnumerator UpdateScore()
    {
        ed = GetComponent<EquipDatabase>();
        tmpGold.text = ed.line.quantity[32].ToString("N0");
        tmpPoint.text = ed.line.quantity[33].ToString("N0");
        yield return null;
    }
}
