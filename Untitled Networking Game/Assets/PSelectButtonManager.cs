using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PSelectButtonManager : NetworkBehaviour
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

        if (isLocalPlayer)
        {
            PlayerPrefs.SetInt("player", runner);

            if (PlayerPrefs.GetInt("player") == runner)
            {
                print("Player 1 is now the runner!");

                isRunner = true;
                isTrapper = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("player", runner);

            if (PlayerPrefs.GetInt("player") == runner)
            {
                print("Player 2 is now the runner!");

                isRunner = true;
                isTrapper = false;
            }
        }
        
        

        //TODO: Don't allow player to select anything else after selecting
    }

    public void TrapperSelect()
    {
        //player prefs set for player to be trapper
        trapper = 2;

        if (isLocalPlayer)
        {
            PlayerPrefs.SetInt("player", trapper);

            if (PlayerPrefs.GetInt("player") == trapper)
            {
                print("Player 1 is now the trapper!");

                isRunner = false;
                isTrapper = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("player", trapper);

            if (PlayerPrefs.GetInt("player") == trapper)
            {
                print("Player 2 is now the trapper!");

                isRunner = false;
                isTrapper = true;
            }
        }
        
        

        //TODO: Don't allow player to select anything else after selecting
    }
}
