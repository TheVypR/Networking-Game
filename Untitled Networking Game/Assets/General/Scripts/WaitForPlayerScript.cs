using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlayerScript : MonoBehaviour
{
    float waitTime = 0.5f;
    public GameObject playerBlind;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SearchPlayers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void bothPlayersFound()
    {
        StopCoroutine(SearchPlayers());
        playerBlind.SetActive(false);
    }

    private IEnumerator SearchPlayers()
    {
        while (true)
        {
            if (FindObjectsOfType<PlayerManagerScript>().Length > 1)
            {
                gameObject.SetActive(false);
                playerBlind.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            
        }
    }
}
