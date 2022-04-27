using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ServerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("isHost"))
        {
            if(PlayerPrefs.GetInt("isHost") == 1)
            {
                NetworkManager.singleton.StartHost();
            } else
            {
                NetworkManager.singleton.StartClient();
                NetworkManager.singleton.networkAddress = "localhost";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
