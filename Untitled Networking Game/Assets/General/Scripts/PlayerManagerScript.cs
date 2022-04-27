using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagerScript : NetworkBehaviour
{
    //authority management
    bool gaveAuth = false;
    //scene management
    int role;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //for testing
        if (isServer && isLocalPlayer)
        {
            PlayerPrefs.SetInt("player", 1);
            CmdGivePrefs();
        } else if(!isServer && isLocalPlayer)
        {
            PlayerPrefs.SetInt("player", 2);
            CmdGivePrefs();
        }
    }

    private void Update()
    {
        if (!gaveAuth)
        {
            if (isServer && isLocalPlayer)
            {
                if (PlayerPrefs.GetInt("player") == 1)
                {
                    PlayerMovementScript p1 = FindObjectOfType<PlayerMovementScript>();
                    if (p1)
                    {
                        print("auth");
                        gaveAuth = true;
                        NetworkIdentity p1ID = p1.gameObject.GetComponent<NetworkIdentity>();
                        p1ID.AssignClientAuthority(connectionToClient);
                    }
                }
                else
                {
                    
                }
            } else if(isServer && !isLocalPlayer)
            {
                Player2Script p2 = FindObjectOfType<Player2Script>();
                if (p2)
                {
                    print("2auth");
                    gaveAuth = true;
                    NetworkIdentity p2ID = p2.gameObject.GetComponent<NetworkIdentity>();
                    p2ID.AssignClientAuthority(connectionToClient);
                }
            }
        }
    }

    [Command]
    void CmdGivePrefs()
    {
        if (PlayerPrefs.HasKey("player"))
        {
            role = PlayerPrefs.GetInt("player");
        } else
        {
            role = 0;
        }
    }
}
