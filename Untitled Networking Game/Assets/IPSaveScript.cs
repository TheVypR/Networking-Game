using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IPSaveScript : MonoBehaviour
{
    public TMP_InputField ip;
    private void Start()
    {
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
