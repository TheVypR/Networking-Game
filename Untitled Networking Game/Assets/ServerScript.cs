using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                        if (PlayerPrefs.GetInt("haveIP") == 0)
                        {
                            NetworkManager.singleton.networkAddress = "localhost";
                        }
                        else
                        {
                            NetworkManager.singleton.networkAddress = PlayerPrefs.GetString("IP");
                        }
                    }
                }
            } else
            {
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
        NetworkIdentity[] ids = Resources.FindObjectsOfTypeAll<NetworkIdentity>();
        foreach (NetworkIdentity id in ids)
        {
            if (id.name.Equals("RespawnCanvas") || id.name.Equals("TransitionCanvas"))
            {
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                id.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
