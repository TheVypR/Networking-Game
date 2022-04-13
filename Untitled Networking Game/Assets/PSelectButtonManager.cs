using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PSelectButtonManager : NetworkBehaviour
{
    int runner;
    int trapper;

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

        if (isLocalPlayer)
        {
            PlayerPrefs.SetInt("player", 1);

            if (PlayerPrefs.GetInt("player") == 1)
            {
                print("Player 1 is now the runner!");
            }
        }
        else
        {
            PlayerPrefs.SetInt("player", 1);

            if (PlayerPrefs.GetInt("player") == 1)
            {
                print("Player 2 is now the runner!");
            }
        }
        

        //TODO: Don't allow player to select anything else after selecting
    }

    public void TrapperSelect()
    {
        //player prefs set for player to be trapper
        
        trapper = 1;

        if (isLocalPlayer)
        {
            PlayerPrefs.SetInt("player", 2);

            if (PlayerPrefs.GetInt("player") == 2)
            {
                print("Player 1 is now the trapper!");
            }
        }
        else
        {
            PlayerPrefs.SetInt("player", 2);

            if (PlayerPrefs.GetInt("player") == 2)
            {
                print("Player 2 is now the trapper!");
            }
        }
        

        //TODO: Don't allow player to select anything else after selecting
    }
}
