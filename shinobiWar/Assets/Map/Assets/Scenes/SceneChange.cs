using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Map()
    {
        SceneManager.LoadScene("Map");
    }
    public void MiniMap1()
    {
        SceneManager.LoadScene("MiniMap1");
    }
    public void MiniMap2()
    {
        SceneManager.LoadScene("MiniMap2");
    }
    public void MiniMap3()
    {
        SceneManager.LoadScene("MiniMap3");
    }
    public void Round1()
    {
        SceneManager.LoadScene("Round 1");
    }
    public void Round2()
    {
        SceneManager.LoadScene("Round 2");
    }
    public void Round3()
    {
        SceneManager.LoadScene("Round 3");
    }
    public void Map2Round1()
    {
        SceneManager.LoadScene("Map2Round1");
    }
    public void Map2Round2()
    {
        SceneManager.LoadScene("Map2Round2");
    }
    public void Map2Round3()
    {
        SceneManager.LoadScene("Map2Round3");
    }
}
