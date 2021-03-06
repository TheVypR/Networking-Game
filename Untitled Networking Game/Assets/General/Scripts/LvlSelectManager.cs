using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlSelectManager : MonoBehaviour
{
    string scenename = "";
    public Canvas lvlSelect;
    public Canvas startLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!scenename.Equals("") && MyInput.GetPS4X(1))
        {
            SceneManager.LoadScene(scenename);
        }
    }

    public void LevelOne()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level1";
            lvlSelect.enabled = false;
            startLevel.enabled = true;
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void LevelTwo()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level2";
            lvlSelect.enabled = false;
            startLevel.enabled = true;
        }
        else
        {
            SceneManager.LoadScene("Level2");
        }
    }

    public void LevelThree()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level3";
            lvlSelect.enabled = false;
            startLevel.enabled = true;
        }
        else
        {
            SceneManager.LoadScene("Level3");
        }
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
        } else
        {
            PlayerPrefs.SetInt("mode", 0);
            print(PlayerPrefs.GetInt("mode"));
        }
        SceneManager.LoadScene("Player Select");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
