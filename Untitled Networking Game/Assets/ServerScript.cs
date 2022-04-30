using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ServerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        print("start");
        if (PlayerPrefs.HasKey("mode"))
        { 
            print("mode");
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
                        //NetworkManager.singleton.networkAddress = "localhost";
                        NetworkManager.singleton.networkAddress = PlayerPrefs.GetString("IP");
                    }
                }
            } else
            {
                print("not online");
                StartCoroutine(EnableIDs());
            }
        } else
        {
            //set it to singleplayer
            PlayerPrefs.SetInt("mode", 0);
            StartCoroutine(EnableIDs());
        }
    }

    IEnumerator EnableIDs()
    {
        print("started");
        NetworkIdentity[] ids = Resources.FindObjectsOfTypeAll<NetworkIdentity>();
        foreach (NetworkIdentity id in ids)
        {
            print("id");
            id.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
