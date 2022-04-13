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
            //check if the players have been selected, if not then send to select scene
            if (PlayerPrefs.GetInt("runner") == 1 && PlayerPrefs.GetInt("trapper") == 2)
            {
                SceneManager.LoadScene("Level1");
            }
            else
            {
                SceneManager.LoadScene("Player Select");
            }


            scenename = "Level1";
            lvlSelect.enabled = false;
            startLevel.enabled = true;
        }
        else
        {
            
            
            
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
            /*
            //check if the players have been selected, if not then send to select scene
            if (PlayerPrefs.GetInt("runner") == 1 && PlayerPrefs.GetInt("trapper") == 2)
            {
                SceneManager.LoadScene("Level2");
            }
            else
            {
                SceneManager.LoadScene("PlayerSelect");
            }
            */
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
            /*
            //check if the players have been selected, if not then send to select scene
            if (PlayerPrefs.GetInt("runner") == 1 && PlayerPrefs.GetInt("trapper") == 2)
            {
                SceneManager.LoadScene("Level3");
            }
            else
            {
                SceneManager.LoadScene("PlayerSelect");
            }
            */
        }


        
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LevelSelect(int mode)
    {
        //if (isMultiplayer)
        //{
        //    PlayerPrefs.SetInt("mode", 1);
        //    if (isOnline)
        //    {
        //        PlayerPrefs.SetInt("online", 1);
        //    } else
        //    {
        //        PlayerPrefs.SetInt("online", 0);
        //    }
        //} else
        //{
        //    PlayerPrefs.SetInt("mode", 0);
        //    PlayerPrefs.SetInt("online", 0);
        //}
        PlayerPrefs.SetInt("mode", mode);
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
