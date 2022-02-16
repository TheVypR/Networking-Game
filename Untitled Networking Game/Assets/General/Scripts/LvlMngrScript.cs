using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LvlMngrScript : MonoBehaviour
{
    public GameObject _player;
    public GameObject _airStrike;
    public GameObject _lavaFlood;
    GameObject lava;

    Transform _playerTrans;
    Rigidbody2D _playerRbody;

    Vector2 spwn;

    //Timers
    public float levelTime;
    float startTime;
    int timeLeft;
    public Text _timer;

    //Win Screen
    public Text _winScreen;
    public Text _loseScreen;


    //Respawn Screen
    public GameObject _respawnCanvas;
    public TMP_Text _respawning;
    public Text _respawnText;
    public Text _respawnTextBack;
    public float _timeRespawn;
    public bool _dead = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();
        startTime = Time.time;
        _timeRespawn = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

        //update timer
        timeLeft = (int)(levelTime - (Time.time - startTime));

        //update timer text
        _timer.text = "Time: " + timeLeft;

        if (timeLeft <= 0)
        {
            //game over
            GameOver(2);
        }

        if (_dead)
        {
            //_respawning.enabled = true;
            //_respawnText.enabled = true;
            //_respawnTextBack.enabled = true;
            _respawnCanvas.SetActive(true);
            if (_timeRespawn > 0)
            {
                _timeRespawn -= Time.deltaTime;
                _respawnText.text = "" + _timeRespawn;
                _respawnTextBack.text = "" + _timeRespawn;

                Debug.Log(_timeRespawn);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(_airStrike);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            lava = Instantiate(_lavaFlood, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 12), Quaternion.identity);
        }
    }

    void Respawn()
    {
        //Handle all respawn timer and texts
        {
            /*_respawning.enabled = false;
            _respawnText.enabled = false;
            _respawnTextBack.enabled = false;*/
            _respawnCanvas.SetActive(false);
            _timeRespawn = 4f;
            _dead = false;
        }


        _player.SetActive(true);

        _playerTrans.position = spwn;
        Camera.main.transform.position = new Vector3(spwn.x, spwn.y, -10);
        _playerRbody.velocity = Vector3.zero;
        _player.GetComponent<PlayerMovementScript>().moveSpeed = 10;
        Destroy(lava);
    }

    public void PlayerDeath(Vector2 spwn)
    {
        this.spwn = spwn;
        _player.SetActive(false);

        _dead = true;
        Invoke("Respawn", 3);
    }

    public void GameOver(int player)
    {
        Destroy(_player);
        if(player == 1)
        {
            _winScreen.enabled = true;
            _loseScreen.enabled = false;
        } else
        {
            _winScreen.enabled = false;
            _loseScreen.enabled = true;
        }
        Invoke("EndGame", 2);
    }

    void EndGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void RespawnTime()
    {
        
    }
}
