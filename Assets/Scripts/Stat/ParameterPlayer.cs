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
    [SerializeField]
    private Stat hpbar;
    
    private string input;
    SwordHitScript swordDamage;
    [SerializeField]
    private TextMeshProUGUI hptext;
    [SerializeField]
    private TextMeshProUGUI leveltext;
    [SerializeField]
    private Stat exp;
    [SerializeField]
    private TextMeshProUGUI exptext;
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
        swordDamage = GetComponent<SwordHitScript>();
        skill = -1;
        ReadAll();
        leveltext.text = getLevel().ToString();
        exp.Initialized(getCur("EXP"), getTotal("EXP"));
        exptext.text = Mathf.Floor((getCur("EXP") / getTotal("EXP")) * 100) + "%";
        hpbar.Initialized(getCur("HP"), getTotal("HP"));
        hptext.text = Mathf.Floor((getCur("HP") / getTotal("HP")) * 100) + "%";
        for (int i = 0; i < 6; i++)
        {
            statSkill[i].Initialized(getCur("TIME" + i.ToString()), getTotal("TIME" + i.ToString()));
        }
    }
    void Update()
    {
        hpbar.Initialized(getCur("HP"), getTotal("HP"));
        if (getCurExp() >= getTotalExp())
        {
            levelUp();
            leveltext.text = getLevel().ToString();
            UpdateExp(getCurExp()-getTotalExp());
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
            hpbar.Initialized(cur, total);
            hptext.text = Mathf.Floor((getCur("HP") / getTotal("HP")) * 100) + "%";
        }
        if (name == "EXP")
        {
            exp.Initialized(cur, total);
            exptext.text = Mathf.Floor((getCur("EXP") / getTotal("EXP")) * 100) + "%";
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
    public void levelUp()
    {
        Change("LV", getCur("LV") + 1, getTotal("LV"));
    }
    public float getCurHeath()
    {
        ReadAll();
        return getCur("HP");
    }
    public float getTotalHealth()
    {
        ReadAll();
        return getTotal("HP");
    }
    public void UpdateHealth(float curHealth)
    {
        ReadAll();
        Change("HP", curHealth, getTotal("HP"));
    }
    public float getCurExp()
    {
        ReadAll();
        return getCur("EXP");
    }
    public float getTotalExp()
    {
        ReadAll();
        return getTotal("EXP");
    }
    public void UpdateExp(float curEXP)
    {
        ReadAll();
        Change("EXP", curEXP, getTotal("EXP"));
    }
    public void AddExp(float addEXP)
    {
        ReadAll();
        Change("EXP", getCur("EXP") + addEXP, getTotal("EXP"));
    }
    public float getDamage(string name)
    {
        ReadAll();
        return getCur(name);
    }
    public void updateDamage(string name, float curDamage)
    {
        ReadAll();
        Change(name, curDamage, curDamage);
    }
    public float getLevel()
    {
        return getCur("LV");
    }
    /*
    HP/10000/10000
    EXP/10000/10000
    LV/1/99
    DMG0/100/100
    DMG1/200/200
    DMG2/400/400
    DMG3/700/700
    TIME0/1/1
    TIME1/3/3
    TIME2/10/10
    TIME3/7/7
    TIME4/15/15
    TIME5/40/40
    */
}
