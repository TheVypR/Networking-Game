using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManagerScript : MonoBehaviour
{
    public GameObject onlineCanvas;
    public GameObject localCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("mode"))
        {
            int mode = PlayerPrefs.GetInt("mode");
            if(mode == 2)
            {
                localCanvas.SetActive(false);
                onlineCanvas.SetActive(true);
            } else if (mode == 1)
            {
                localCanvas.SetActive(true);
                onlineCanvas.SetActive(false);
            } else
            {
                localCanvas.SetActive(false);
                onlineCanvas.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
