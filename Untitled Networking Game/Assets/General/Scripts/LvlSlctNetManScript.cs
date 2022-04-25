using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LvlSlctNetManScript : NetworkBehaviour
{
    public NetworkManager netMan;
    public GameObject hostSelctCanvas;
    public GameObject level1Button;
    public GameObject hostButton;

    private void Start()
    {
        netMan = GetComponent<NetworkManager>();
        EventSystem.current.SetSelectedGameObject(hostButton);
    }

    public void HostButtonClick()
    {
        netMan.StartHost();
        hostSelctCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(level1Button);
        PlayerPrefs.SetInt("player", 1);
    }
    public void ClientButtonClick()
    {
        netMan.StartClient();
        hostSelctCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(level1Button);
        PlayerPrefs.SetInt("player", 2);
    }


}
