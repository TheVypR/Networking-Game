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
    }


    public void saveToPrefs()
    {
        PlayerPrefs.SetString("IP", ip.text);
    }
}
