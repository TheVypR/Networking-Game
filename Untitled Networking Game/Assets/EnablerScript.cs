using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class EnablerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("mode"))
        {
            if (PlayerPrefs.GetInt("mode") != 2)
            {
                StartCoroutine(EnableIDs());
            }
        }
        else
        {
            //set it to singleplayer
            PlayerPrefs.SetInt("mode", 0);
            StartCoroutine(EnableIDs());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
