using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ServerScript : MonoBehaviour
{
    NetworkManager netMan;

    // Start is called before the first frame update
    void Start()
    {
        netMan = gameObject.GetComponent<NetworkManager>();
        if (PlayerPrefs.HasKey("isHost"))
        {
            if (PlayerPrefs.GetInt("isHost") == 1)
            {
                netMan.StartHost();
            } else
            {
                netMan.StartClient();
                //set the IP
                netMan.networkAddress = "localhost";
            }
        } else
        {
            //not online mode
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
