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
            if (isServer && isLocalPlayer)
            {
                print("serverAuth");
                if (PlayerPrefs.GetInt("player") == 1)
                {
                    print("auth1");
                    PlayerMovementScript p1 = FindObjectOfType<PlayerMovementScript>();
                    NetworkIdentity p1ID = p1.gameObject.GetComponent<NetworkIdentity>();
                    p1ID.AssignClientAuthority(connectionToClient);
                }
                else if (PlayerPrefs.GetInt("player") == 2)
                {
                    print("auth2");
                    FindObjectOfType<Player2Script>().gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
                }
                else
                {
                    print("ERROR: No player number found");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
