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

    [ClientRpc]
    void RpcSetLevel(string lvl)
    {
        level = lvl;
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (player1.hasAuthority)
        {
            setPlayerOneBlind(true);
        }
    }

}