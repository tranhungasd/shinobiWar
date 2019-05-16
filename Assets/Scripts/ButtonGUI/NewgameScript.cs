using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class NewgameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string newgamestats;
        string newpath = Application.dataPath + "/Saves/newGame.txt";
        string curpath = Application.dataPath + "/Saves/systemSave.txt";
        newgamestats = File.ReadAllText(newpath);
        File.WriteAllText(curpath, newgamestats);
        SceneManager.LoadScene(Application.dataPath + "/Scenes/Minimap1/Round1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}