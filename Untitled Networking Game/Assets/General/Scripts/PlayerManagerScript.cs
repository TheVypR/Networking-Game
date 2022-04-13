using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagerScript : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("player"))
        {
            if (isServer)
            {
                if (isLocalPlayer)
                {
                    print("serverAuth");
                    PlayerMovementScript p1 = FindObjectOfType<PlayerMovementScript>();
                    if (p1)
                    {
                        NetworkIdentity p1ID = p1.gameObject.GetComponent<NetworkIdentity>();
                        p1ID.AssignClientAuthority(connectionToClient);
                        NetworkServer.AddPlayerForConnection(connectionToClient, gameObject);
                    }
                } else
                {
                    print("clientAuth");
                    Player2Script p2 = FindObjectOfType<Player2Script>();
                    if (p2)
                    {
                        NetworkIdentity p2ID = p2.gameObject.GetComponent<NetworkIdentity>();
                        p2ID.AssignClientAuthority(connectionToClient);
                        NetworkServer.AddPlayerForConnection(connectionToClient, gameObject);
                    }
                }
            }
        }
    }
}
