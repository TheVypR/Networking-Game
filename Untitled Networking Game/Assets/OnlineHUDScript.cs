using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OnlineHUDScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHost()
    {
        PlayerPrefs.SetInt("isHost", 1);
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnJoin()
    {
        PlayerPrefs.SetInt("isHost", 0);
        SceneManager.LoadScene("MatchWaitScene");
    }
}
