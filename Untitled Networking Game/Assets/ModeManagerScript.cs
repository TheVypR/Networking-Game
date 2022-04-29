using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManagerScript : MonoBehaviour
{
    public GameObject localCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("mode"))
        {
            int mode = PlayerPrefs.GetInt("mode");
            if(mode == 1)
            {
                localCanvas.SetActive(true);
            } else
            {
                localCanvas.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
