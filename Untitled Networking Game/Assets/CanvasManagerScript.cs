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

    bool blindPlayerOne = true;

    void Start()
    {
        StartCoroutine(SearchPlayers());

        if (player1.hasAuthority)
        {
            StartCoroutine(CheckIfBlind());
        }
        
    }

    public void setPlayerOneBlind(bool set)
    {
        blindPlayerOne = set;
    }

    private void bothPlayersFound()
    {
        StopCoroutine(SearchPlayers());
        playerWaitingBlind.SetActive(false);
    }

    private void stopPlayer1Blind()
    {
        StopCoroutine(CheckIfBlind());
        setupPanel.SetActive(false);
        setupText.SetActive(false);
    }


    private IEnumerator SearchPlayers()
    {
        while (true)
        {
            if (FindObjectsOfType<PlayerManagerScript>().Length > 1)
            {
                bothPlayersFound();
            } else
            {
                playerWaitingBlind.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);

        }
    }

    private IEnumerator CheckIfBlind()
    {
        while (true)
        {
            if (!blindPlayerOne)
            {
                stopPlayer1Blind();
            } else
            {
                setupText.SetActive(true);
                setupPanel.SetActive(true);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
