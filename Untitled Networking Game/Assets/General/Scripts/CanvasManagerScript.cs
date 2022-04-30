using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasManagerScript : NetworkBehaviour
{
    string level = "";
    public NetworkIdentity player1;
    public GameObject setupText;
    public GameObject setupPanel;

    float waitTime = 0.1f;
    public GameObject playerWaitingBlind;
    public LvlMngrScript levelmanager;

    void Start()
    {
        if (PlayerPrefs.HasKey("mode"))
        {
            if (PlayerPrefs.GetInt("mode") == 2)
            {
                StartCoroutine(LateStart(0.1f));
            } else
            {
                return;
            }
        } else
        {
            return;
        }
    }

    public void setPlayerOneBlind(bool set)
    {
        if (setupText && setupPanel)
        {
            setupText.SetActive(set);
            setupPanel.SetActive(set);
        }
    }

    private void bothPlayersFound()
    {
        StopCoroutine(SearchPlayers());
        playerWaitingBlind.SetActive(false);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        if (isServer && isLocalPlayer)
        {
            if (PlayerPrefs.HasKey("level"))
            {
                RpcSetLevel(PlayerPrefs.GetString("level"));
            }
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
    }

    [ClientRpc]
    void RpcSetLevel(string lvl)
    {
        level = lvl;
    }

    private IEnumerator SearchPlayers()
    {
        while (true)
        {
            if (FindObjectsOfType<PlayerManagerScript>().Length > 1)
            {
                bothPlayersFound();
            }
            else
            {
                
                playerWaitingBlind.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);

        }
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(SearchPlayers());

        if (player1.hasAuthority)
        {
            setPlayerOneBlind(true);
        }
    }

}