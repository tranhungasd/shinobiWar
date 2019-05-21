
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
public class MissionLoader : MonoBehaviour
{
    int totalkills;
    public GameObject Player;
    ParameterPlayer readStats;
    string mission_pool_path;
    string mission_des_path;
    string mission_content;
    string mission_description;
    int chosen;
    bool success = true;
    float reward;
    float lastHP;
    bool HPStay = true;
    bool usedSkill = false;
    public TextMeshProUGUI missionDescription;
    public TextMeshProUGUI goldreward;
    public TextMeshProUGUI expreward;
    public TextMeshProUGUI pointsreward;
    private float earngold = 10000;
    private float earnexp = 200000;
    private float earnpoints = 150000;
    private class mssionParameter
    {
        public string[] name = new string[20];
        public float[] current = new float[20];
        public float[] total = new float[20];
        public int count;
    }
    private mssionParameter missions = new mssionParameter();
    string[] descriptions = new string[8];
    // Start is called before the first frame update
    void Start()
    {
        readStats = Player.gameObject.GetComponent<ParameterPlayer>();
        mission_pool_path = Application.dataPath + "/Saves/missionPool.txt";
        mission_des_path = Application.dataPath + "/Saves/missionDes.txt";
        mission_content = File.ReadAllText(mission_pool_path);
        mission_description = File.ReadAllText(mission_des_path);
        goldreward.text = earngold.ToString();
        expreward.text = earnexp.ToString();
        pointsreward.text = earnpoints.ToString();
        ReadAll();
        missionGenerator();
        writeDes();
    }

    // Update is called once per frame
    void Update()
    {

        writeDes();
    }
    private void ReadAll()
    {
        string pattern = "\n";
        int i = 0;
        string[] elements1 = Regex.Split(mission_content, pattern);
        string[] elements2 = Regex.Split(mission_description, pattern);
        foreach (string m in elements1)
        {
            if (m != "")
            {
                missions.count = i;
                string pat = "/";
                string[] tags = Regex.Split(m, pat);
                missions.name[i] = tags[0];
                missions.current[i] = float.Parse(tags[1]);
                missions.total[i] = float.Parse(tags[2]);
                //Debug.Log(tags[1] );
                //Debug.Log(line.count + "-" + line.name[i] + "-" + line.current[i].ToString() + "-" + line.total[i].ToString() + "\n");
                i++;
            }
        }
        int j = 0;
        foreach (string n in elements2)
        {
            descriptions[j] = n;
            j++;
        }
    }
    private float getCur(string str)
    {
        for (int i = 0; i <= missions.count; i++)
        {
            if (missions.name[i] == str)
            {
                return missions.current[i];
            }
        }
        return -1; //Not Found
    }
    private float getTotal(string str)
    {
        for (int i = 0; i <= missions.count; i++)
        {
            if (missions.name[i] == str)
            {
                return missions.total[i];
            }
        }
        return -1; //Not Found
    }
    private void missionGenerator()
    {
        System.Random rnd = new System.Random();
        chosen = rnd.Next(7);
    }
    private void UpdateMission()
    {

    }
    private bool CheckMissionFail(int missionindex)
    {
        if (missionindex >= 3)
        {
            if (usedSkill == true) return true;
        }
        switch (missionindex)
        {
            case 0:
                {
                    if (totalkills <= getTotal("KILLS"))
                    {
                        return true;
                    }
                    break;
                }
            case 1:
                {
                    if (lastHP <= getTotal("HPLEFT"))
                    {
                        return true;
                    }
                    break;
                }
            case 2:
                {
                    if (HPStay == false)
                    {
                        return true;
                    }
                    break;
                }
        }
        return false;
    }
    private void GiveReward()
    {
        readStats.AddExp(earnexp);
        readStats.addScore(earnpoints);

    }
    public void addKill()
    {
        totalkills++;
    }
    public void setLastHP(float playerlastHP)
    {
        lastHP = playerlastHP;
    }
    public void checkDropHP(float curHP)
    {
        Debug.Log(curHP);
        Debug.Log(getTotal("HPDROP"));
        if (curHP <= getTotal("HPDROP"))
        {
            HPStay = false;
        }
    }
    public void skillUsed(int skill)
    {
        Debug.Log(skill.ToString());
        Debug.Log(missions.name[chosen].Substring(missions.name[chosen].Length - 1, 1));

        if (skill.ToString() == missions.name[chosen].Substring(missions.name[chosen].Length - 1, 1))
        {
            usedSkill = true;
        }
    }
    private void writeDes()
    {
        missionDescription.text = descriptions[chosen];
        
        bool failed = CheckMissionFail(chosen);
        if (chosen == 0)
        {
            missionDescription.text = String.Concat(descriptions[chosen], "\n", "current: ", totalkills);
        }
        else if (failed && chosen != 1)
        {
            missionDescription.text = String.Concat(descriptions[chosen], "\n", "FAILED");
        }
    }
}