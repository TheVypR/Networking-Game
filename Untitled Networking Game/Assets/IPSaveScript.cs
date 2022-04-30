using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IPSaveScript : MonoBehaviour
{
    public GameObject ipTextField;
    private TMP_Text ip;
    private void Start()
    {
        ip = ipTextField.GetComponent<TMP_Text>();
        PlayerPrefs.SetInt("haveIP", 0);
    }


    public void saveToPrefs()
    {
        if (ip.text.Length > 6)
        {
            PlayerPrefs.SetInt("haveIP", 1);
            PlayerPrefs.SetString("IP", ip.text);
        }
        else
        {
            print("Not an IP, must be longer than 6 characters.");
        }
    }
}
