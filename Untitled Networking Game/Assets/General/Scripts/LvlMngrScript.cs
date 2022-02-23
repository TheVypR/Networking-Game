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
    public TMP_Text _deathText;
    public Text _addPlusOne;
    public float _timeRespawn;
    public bool _dead = false;
    public int _countDeaths;
    private float _textRise;

    // Start is called before the first frame update
    void Start()
    {
        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();
        startTime = Time.time;
        _timeRespawn = 4f;
        _countDeaths = 0;
        _textRise = 0;
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
            _deathText.text = "Death Count: " + _countDeaths;

            if (_timeRespawn > 0)
            {
                if (_textRise < 5)
                {
                    _textRise += 0.01f;
                }
                _addPlusOne.GetComponent<Text>().transform.position = new Vector3(1400, (_textRise * 200) + 250, 0);
                
                _timeRespawn -= Time.deltaTime;
                _respawnText.text = "" + _timeRespawn;
                _respawnTextBack.text = "" + _timeRespawn;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(_airStrike);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            lava = Instantiate(_lavaFlood, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 20), Quaternion.identity);
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
            _textRise = 0;
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

        Invoke("CountDeath", 1);
        Invoke("Respawn", 3.5f);
        Invoke("isDead", 0.5f);
    }

    void isDead()
    {
        _dead = true;
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

    public void CountDeath()
    {
        _countDeaths += 1;
    }
}
