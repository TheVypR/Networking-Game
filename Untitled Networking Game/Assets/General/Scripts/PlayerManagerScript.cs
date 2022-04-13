using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagerScript : NetworkBehaviour
{
    bool gaveAuth = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("player") && !gaveAuth)
        {
            if (isServer)
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
                        NetworkServer.AddPlayerForConnection(connectionToClient, gameObject);
                    }
                }
                else
                {
                    Player2Script p2 = FindObjectOfType<Player2Script>();
                    if (p2)
                    {
                        print("2auth");
                        gaveAuth = true;
                        NetworkIdentity p2ID = p2.gameObject.GetComponent<NetworkIdentity>();
                        p2ID.AssignClientAuthority(connectionToClient);
                        NetworkServer.AddPlayerForConnection(connectionToClient, gameObject);
                    }
                }
            }
        }
    }
}
