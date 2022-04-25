using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class LvlSelectManager : NetworkBehaviour
{
    string scenename = "";
    public GameObject lvlSelect;
    public GameObject startLevel;

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

    [ClientRpc]
    void RpcLoadScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
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
            lvlSelect.SetActive(false);
            startLevel.SetActive(true);
        }
        else
        {
            if (isServer)
            {
                SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
                RpcLoadScene("Level1");
            }
        }


        
    }

    public void LevelTwo()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level2";
            lvlSelect.SetActive(false);
            startLevel.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync("Level2", LoadSceneMode.Additive);
            RpcLoadScene("Level2");
        }


        
    }

    public void LevelThree()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            scenename = "Level3";
            lvlSelect.SetActive(false);
            startLevel.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync("Level3", LoadSceneMode.Additive);
            RpcLoadScene("Level3");
        }


        
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //used on Main Menu
    public void LevelSelect(int mode)
    {
        PlayerPrefs.SetInt("mode", mode);
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
