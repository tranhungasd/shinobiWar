using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;

public class ParameterPlayer : MonoBehaviour
{
    public TextMeshProUGUI tmpTxt;
    private string input;
    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat exp;
    [SerializeField]
    private Stat[] statSkill = new Stat[6];
    public int skill { get; set; }
    public bool[] waitRecSkill;
    private class tagParameter
    {
        public string[] name = new string[20];
        public float[] current = new float[20];
        public float[] total = new float[20];
        public int count;
    }
    private tagParameter line = new tagParameter();
    void Start()
    {
        skill = -1;
        ReadAll();
        health.Initialized(getCur("HP"), getTotal("HP"));
        exp.Initialized(getCur("EXP"), getTotal("EXP"));
        for (int i = 0; i < 6; i++)
        {
            statSkill[i].Initialized(getCur("TIME" + i.ToString()), getTotal("TIME" + i.ToString()));
        }
    }
    void Update()
    {
        //ReadAll();
        useSkill();
        if (Input.GetKey(KeyCode.G))
        {
            line.current[0] = line.current[0] - 10;
            Change("HP", line.current[0], line.total[0]);
            //Debug.Log(line.current[0].ToString());
        }
        if (Input.GetKey(KeyCode.H))
        {
            line.current[1] = line.current[1] - 10;
            Change("EXP", line.current[1], line.total[1]);
            //Debug.Log(line.current[0].ToString());
        }
    }
    private void useSkill()
    {
        if (skill == -1)
        {
            return;
        }
        Debug.Log("skill " + skill);
        StartCoroutine(waitSkill(skill));
        skill = -1;
    } 
    IEnumerator waitSkill(int stt)
    {
        waitRecSkill[stt] = true;
        float cur = getCur("TIME" + stt.ToString()) * 10;
        for (float i = cur; i >= 0; i--)
        {
            statSkill[stt].Initialized(i, getTotal("TIME" + stt.ToString()) * 10);
            yield return new WaitForSeconds(0.1f);
        }
        waitRecSkill[stt] = false;
    }
    private void ReadAll()  
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
                line.name[i] = tags[0];
                line.current[i] = float.Parse(tags[1]);
                line.total[i] = float.Parse(tags[2]);
                //Debug.Log(tags[1] );
                //Debug.Log(line.count + "-" + line.name[i] + "-" + line.current[i].ToString() + "-" + line.total[i].ToString() + "\n");
                i++;
            }
        }
    }
    private float getCur(string str)
    {
        for (int i = 0; i <= line.count; i++)
        {
            if (line.name[i] == str)
            {
                return line.current[i];
            }
        }
        return -1; //Not Found
    }
    private float getTotal(string str)
    {
        for (int i = 0; i <= line.count; i++)
        {
            if (line.name[i] == str)
            {
                return line.total[i];
            }
        }
        return -1; //Not Found
    }
    IEnumerator updateStat(string name, float cur, float total)
    {
        if (name == "HP")
        {
            health.Initialized(cur, total);
        }
        if (name == "EXP")
        {
            exp.Initialized(cur, total);
        }
        yield return null;
    }
    IEnumerator reWrite()
    {
        tmpTxt.text = "";
        for (int i = 0; i <= line.count; i++)
        {
            tmpTxt.text += line.name[i] + "/" + line.current[i] + "/" + line.total[i] + "\n";
        }
        yield return null;
    }
    private void Change(string name, float cur, float total)
    {
        for (int i = 0; i <= line.count; i++)
        {
            if (line.name[i] == name)
            {
                line.current[i] = cur;
                line.total[i] = total;
                break;
            }
        }
        StartCoroutine(reWrite());
        StartCoroutine(updateStat(name, cur, total));
    } 
/*
HP/10000/10000
EXP/10000/10000
LV/1/1
DMG0/100/100
DMG1/200/200
DMG2/500/500
DMG3/700/700
DMG4/1000/1000
DMG5/2500/2500
TIME0/1/1
TIME1/3/3
TIME2/10/10
TIME3/7/7
TIME4/15/15
TIME5/40/40
*/
}
