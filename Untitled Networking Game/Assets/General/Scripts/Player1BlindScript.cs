using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Player1BlindScript : NetworkBehaviour
{

    public NetworkIdentity player1;
    public GameObject setupText;
    public GameObject setupPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(player1.hasAuthority)
        {
            setupText.SetActive(false);
            setupPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
