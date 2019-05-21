using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;
using System.Linq;
using UnityEngine.SceneManagement;

public class ParameterPlayer : MonoBehaviour
{
    public TextMeshProUGUI tmpTxt;
    public GameObject missionControl;
    [SerializeField]
    private Stat hpbar;
    private string scenePath;
    public TextMeshProUGUI[] descriptions = new TextMeshProUGUI[5];
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

        string path = Application.dataPath + "/Saves/systemSave.txt";
        tmpTxt.text = File.ReadAllText(path);
        swordDamage = GetComponent<SwordHitScript>();
        skill = -1;
        ReadAll();
        saveStats();
        leveltext.text = getLevel().ToString();
        exp.Initialized(getCur("EXP"), getTotal("EXP"));
        exptext.text = Mathf.Floor((getCur("EXP") / getTotal("EXP")) * 100) + "%";
        hpbar.Initialized(getCur("HP"), getTotal("HP"));
        hptext.text = Mathf.Floor((getCur("HP") / getTotal("HP")) * 100) + "%";
        for (int i = 0; i < 6; i++)
        {
            statSkill[i].Initialized(0, getTotal("TIME" + i.ToString()));
        }
    }
    void Update()
    {
        displayDamage();
        hpbar.Initialized(getCur("HP"), getTotal("HP"));
        if (getCurExp() >= getTotalExp())
        {
            levelUp();
            leveltext.text = getLevel().ToString();
            UpdateExp((getCur("EXP") % getTotal("EXP")));
        }
        saveStats();
    }
    public void useSkill()
    {
        if (skill == -1)
        {
            return;
        }
        missionControl.GetComponent<MisionLoader>().skillUsed(skill);
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
    public void ReadAll()
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

        Debug.Log("read");
        ReadAll();
    }
    public void levelUp()
    {
        Change("LV", getCur("LV") + Mathf.Floor(getCur("EXP") / getTotal("EXP")), getTotal("LV"));
        AddDamage("DMG0", 20);
        if (getLevel() >= 3)
        {
            AddDamage("DMG1", 40);
        }
        if (getLevel() >= 6)
        {
            AddDamage("DMG3", 70);
        }
        if (getLevel() >= 12)
        {
            AddDamage("DMG5", 100);
        }
        displayDamage();
    }
    private void displayDamage()
    {
        if (getLevel() >= 3)
        {
            descriptions[0].text = "Damage " + getDamage("DMG1");
        }
        if (getLevel() >= 5)
        {
            descriptions[1].text = "Damage " + getDamage("DMG2");
        }
        if (getLevel() >= 6)
        {
            descriptions[2].text = "Damage " + getDamage("DMG3");
        }
        if (getLevel() >= 8)
        {
            descriptions[3].text = "Damage " + getDamage("DMG4");
        }
        if (getLevel() >= 12)
        {
            descriptions[4].text = "Damage " + getDamage("DMG5");
        }
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
    public void AddDamage(string name, float addedDamage)
    {
        ReadAll();
        Change(name, getCur(name) + addedDamage, getCur(name) + addedDamage);
    }
    public void AddHP(string name, float addedHP)
    {
        ReadAll();
        float curHp = getCur(name) + addedHP;
        if (curHp >= getTotalHealth())
        {
            curHp = getTotalHealth();
        }
        Change(name, curHp, getTotalHealth());
    }
    public void HPUP(string name, float addedHP)
    {
        ReadAll();
        float curHp = getTotal(name) + addedHP;
        Change(name, curHp, curHp);
        ReadAll();
    }
    public float getScore()
    {
        return getCur("SCORE");
    }
    public void addScore(float score)
    {
        Change("SCORE", getScore() + score, getScore() + score);
    }
    public float getLevel()
    {
        return getCur("LV");
    }

    public void saveStats()
    {
        string path = Application.dataPath + "/Saves/systemSave.txt";
        ReadAll();
        // This text is added only once to the file.
        if (File.Exists(path))
        {
            File.WriteAllText(path, input);
        }
        string currentScenePath = Application.dataPath + "/Saves/currentScene.txt";
        scenePath = SceneManager.GetActiveScene().path;
        if (File.Exists(currentScenePath))
        {
            File.WriteAllText(currentScenePath, scenePath);
        }
    }
}