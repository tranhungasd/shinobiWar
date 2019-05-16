using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class ContinueGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string curpathscene = Application.dataPath + "/Saves/currentScene.txt";
        SceneManager.LoadScene(curpathscene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}