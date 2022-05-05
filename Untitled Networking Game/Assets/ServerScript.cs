using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class ServerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("mode"))
        { 
            if (PlayerPrefs.GetInt("mode") == 2)
            {
                if (PlayerPrefs.HasKey("isHost"))
                {
                    if (PlayerPrefs.GetInt("isHost") == 1)
                    {
                        NetworkManager.singleton.StartHost();
                    }
                    else
                    {
                        NetworkManager.singleton.StartClient();
                        if (PlayerPrefs.HasKey("IP"))
                        {
                            NetworkManager.singleton.networkAddress = PlayerPrefs.GetString("IP");
                        }
                        else
                        {
                            NetworkManager.singleton.networkAddress = "localhost";
                        }
                    }
                }
            }
        } else
        {
            //set it to singleplayer
            PlayerPrefs.SetInt("mode", 0);
        }
    }
}
