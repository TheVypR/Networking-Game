using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WaitingForPlayerScript : NetworkBehaviour
{
    string level = "";
    float waitTime = 0.1f;
    Canvas waiting;

    void Start()
    {
        waiting = GetComponent<Canvas>();
        StartCoroutine(LateStart(0.1f));
        waiting.enabled = true;
    }

    private void bothPlayersFound()
    {
        if (isServer)
        {
            if (PlayerPrefs.HasKey("level"))
            {
                NetworkManager.singleton.ServerChangeScene(PlayerPrefs.GetString("level"));
            }
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1f);
        waiting.enabled = false;
        NetworkManager.singleton.ServerChangeScene(level);
    }

    [ClientRpc]
    void RpcSetLevel(string lvl)
    {
        level = lvl;
        print(level);
    }

    private IEnumerator SearchPlayers()
    {
        while (FindObjectsOfType<PlayerManagerScript>().Length < 2)
        {
            yield return new WaitForSeconds(waitTime);
        }
        bothPlayersFound();
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(SearchPlayers());
    }

}