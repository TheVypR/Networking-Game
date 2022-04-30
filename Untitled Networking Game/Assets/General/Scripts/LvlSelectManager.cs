using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class LvlSelectManager : MonoBehaviour
{
    string scenename = "";
    public GameObject lvlSelect;
    public GameObject startLevel;
    public GameObject onlineHUD;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //used to check local multiplayer button press
        if (!scenename.Equals("") && MyInput.GetPS4X(1))
        {
            SceneManager.LoadScene(scenename);
        }
    }

    public void LevelOne()
    {
        //singleplayer
        if(PlayerPrefs.GetInt("mode") == 0)
        {
            SceneManager.LoadScene("Level1");
        }
        //local multiplayer
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level1";
            lvlSelect.SetActive(false);
            startLevel.SetActive(true);
        }
        //online multiplayer
        if (PlayerPrefs.GetInt("mode") == 2)
        {
            PlayerPrefs.SetString("level", "Level1");
            SceneManager.LoadScene("Player Select");
        }
    }

    public void LevelTwo()
    {
        //singleplayer
        if (PlayerPrefs.GetInt("mode") == 0)
        {
            SceneManager.LoadScene("Level2");
        }
        //local multiplayer
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level2";
            lvlSelect.SetActive(false);
            startLevel.SetActive(true);
        }
        //online multiplayer
        if (PlayerPrefs.GetInt("mode") == 2)
        {
            PlayerPrefs.SetString("level", "Level2");
            SceneManager.LoadScene("Player Select");
        }
    }

    public void LevelThree()
    {
        //singleplayer
        if (PlayerPrefs.GetInt("mode") == 0)
        {
            SceneManager.LoadScene("Level3");
        }
        //local multiplayer
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level3";
            lvlSelect.SetActive(false);
            startLevel.SetActive(true);
        }
        //online multiplayer
        if (PlayerPrefs.GetInt("mode") == 2)
        {
            PlayerPrefs.SetString("level", "Level3");
            SceneManager.LoadScene("Player Select");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //used on Main Menu
    public void LevelSelect(int mode)
    {
        //0 = singleplayer
        //1 = local MP
        //2 = online MP
        PlayerPrefs.SetInt("mode", mode);
        if(mode == 2)
        {
            onlineHUD.SetActive(true);
        } else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
