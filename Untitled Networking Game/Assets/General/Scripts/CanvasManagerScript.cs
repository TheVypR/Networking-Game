using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CanvasManagerScript : NetworkBehaviour
{

    public NetworkIdentity player1;
    public GameObject setupText;
    public GameObject setupPanel;

    float waitTime = 0.5f;
    public GameObject playerWaitingBlind;

    void Start()
    {
        StartCoroutine(LateStart(1.0f));
    }


    //void Start()
    //{
    //    StartCoroutine(SearchPlayers());


    //    if (player1.hasAuthority)
    //    {
    //        print("Have Authority");
    //        setPlayerOneBlind(true);
    //    }

    //}


    public void setPlayerOneBlind(bool set)
    {
        setupText.SetActive(set);
        setupPanel.SetActive(set);
    }

    private void bothPlayersFound()
    {
        StopCoroutine(SearchPlayers());
        playerWaitingBlind.SetActive(false);
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
            print("Have Authority");
            setPlayerOneBlind(true);
        }
    }

}