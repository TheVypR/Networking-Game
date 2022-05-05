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
        if (isServer)
        {
            role = PlayerPrefs.GetInt("player");
        }
    }

    private void Update()
    {
        if (!gaveAuth)
        {
            if (isServer && isLocalPlayer)
            {
                if (role == 1)
                {
                    PlayerMovementScript p1 = FindObjectOfType<PlayerMovementScript>();
                    if (p1)
                    {
                        gaveAuth = true;
                        NetworkIdentity p1ID = p1.gameObject.GetComponent<NetworkIdentity>();
                        p1ID.AssignClientAuthority(connectionToClient);
                    }
                }
                else if (role == 2)
                {
                    Player2Script p2 = FindObjectOfType<Player2Script>();
                    if (p2)
                    {
                        gaveAuth = true;
                        NetworkIdentity p2ID = p2.gameObject.GetComponent<NetworkIdentity>();
                        p2ID.AssignClientAuthority(connectionToClient);
                    }
                }
                else
                {

                }
            }
            else if (isServer && !isLocalPlayer)
            {
                print("client");
                if (role == 2)
                {
                    print("roling");
                    PlayerMovementScript p1 = FindObjectOfType<PlayerMovementScript>();
                    if (p1)
                    {
                        print("roling2");
                        gaveAuth = true;
                        NetworkIdentity p1ID = p1.gameObject.GetComponent<NetworkIdentity>();
                        p1ID.AssignClientAuthority(connectionToClient);
                    }
                }
                else if (role == 1)
                {
                    print("roling");
                    Player2Script p2 = FindObjectOfType<Player2Script>();
                    if (p2)
                    {
                        print("roling2");
                        gaveAuth = true;
                        NetworkIdentity p2ID = p2.gameObject.GetComponent<NetworkIdentity>();
                        p2ID.AssignClientAuthority(connectionToClient);
                    }
                }
                else
                {

                }
            }
        }
    }
}
