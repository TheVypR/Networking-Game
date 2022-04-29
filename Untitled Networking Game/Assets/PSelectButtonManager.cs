using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class PSelectButtonManager : MonoBehaviour
{
    public NetworkManager netMan;


    int runner = 0;
    int trapper = 0;

    public bool isRunner = false;
    public bool isTrapper = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunnerSelect()
    {
        //player prefs set for player to be runner
        runner = 1;
        trapper = 0;

        PlayerPrefs.SetInt("player", runner);

        if (PlayerPrefs.GetInt("player") == runner)
        {
            print("You are the runner!");

            isRunner = true;
            isTrapper = false;
        }
        
        

        //TODO: Don't allow player to select anything else after selecting

        if (isRunner == true && isTrapper == false)
        {
            SceneManager.LoadScene("MatchWaitScene");
        }
    }

    public void TrapperSelect()
    {
        //player prefs set for player to be trapper
        trapper = 2;
        runner = 0;

        PlayerPrefs.SetInt("player", trapper);

        if (PlayerPrefs.GetInt("player") == trapper)
        {
            print("You are the trapper!");

            isRunner = false;
            isTrapper = true;
        }



        //TODO: Don't allow player to select anything else after selecting

        if (isRunner == false && isTrapper == true)
        {
            SceneManager.LoadScene("MatchWaitScene");
        }
    }
}
