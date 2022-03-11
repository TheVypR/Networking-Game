using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LevelSelect(bool isMultiplayer)
    {
        print(isMultiplayer);
        if (isMultiplayer)
        {
            print(isMultiplayer);
            PlayerPrefs.SetInt("mode", 1);
            print(PlayerPrefs.GetInt("mode"));
        } else
        {
            PlayerPrefs.SetInt("mode", 0);
            print(PlayerPrefs.GetInt("mode"));
        }
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
