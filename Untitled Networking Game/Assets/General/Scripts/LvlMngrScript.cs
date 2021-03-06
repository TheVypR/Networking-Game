using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LvlMngrScript : MonoBehaviour
{
    //check multiplier
    int isMultiplayer;
    public GameObject player2;
    public GameObject singleplayerAI;
    public EconomyScript economyScript;

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

    //setup or gameplay
    public GameObject transitionCanvas;
    public CameraMotor _camMotor;
    bool isSetup = false;
    public float setupTimer = 30;
    float setupStart;

    //Respawn Screen
    public GameObject _respawnCanvas;
    TMP_Text _respawning;
    Text _respawnText;
    Text _respawnTextBack;
    TMP_Text _deathText;
    Text _addPlusOne;
    float _timeRespawn;
    bool _dead = false;
    int _countDeaths;
    float _textRise;

    // Start is called before the first frame update
    void Start()
    {
        //check if multiplayer
        if (PlayerPrefs.HasKey("mode"))
        {
            isMultiplayer = PlayerPrefs.GetInt("mode");
            //disable trap AI
            if (isMultiplayer == 1)
            {
                singleplayerAI.SetActive(false);
                _player.SetActive(false);
                player2.SetActive(true);
                _camMotor.setMode(true);
            } else
            {
                player2.SetActive(false);
                singleplayerAI.SetActive(true);
                _camMotor.setMode(false);
            }
        } else
        {
            PlayerPrefs.SetInt("mode", 0);
            isMultiplayer = 0;
            _camMotor.setMode(false);
            singleplayerAI.SetActive(true);
        }

        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();

        //only start round if singleplayer
        if (isMultiplayer == 0) { startTime = Time.time; }

        _timeRespawn = 4f;
        _countDeaths = 0;
        _textRise = 0;

        if (isMultiplayer == 1)
        {
            isSetup = true;
            setupStart = Time.time;
        }

        //respawn canvas children
        _respawnText = _respawnCanvas.transform.Find("RespawnCountdownText").gameObject.GetComponent<Text>();
        _respawnTextBack = _respawnCanvas.transform.Find("RespawnCountdownText (1)").gameObject.GetComponent<Text>();
        _respawning = _respawnCanvas.transform.Find("RespawningText").gameObject.GetComponent<TMP_Text>();
        _deathText = _respawnCanvas.transform.Find("Death Count").gameObject.GetComponent<TMP_Text>();
        _addPlusOne = _respawnCanvas.transform.Find("AddOneDeathText").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

        //update timer text
        if (isSetup)
        {
            //update timer
            timeLeft = (int)(setupTimer - (Time.time - setupStart));
            if (isMultiplayer == 1 && timeLeft <= 0)
            {
                isSetup = false;
                //load intermediary canvas and pause time
                Time.timeScale = 0;
                transitionCanvas.SetActive(true);
            }
        }
        else
        {
            //update timer
            timeLeft = (int)(levelTime - (Time.time - startTime));
            if (timeLeft <= 0)
            {
                //game over
                GameOver(2);
            }
        }
        _timer.text = "Time: " + timeLeft;

        if (_dead)
        {
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
    }

    void Respawn()
    {
        //Handle all respawn timer and texts
        {
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

        _countDeaths++;
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

    public void StartRound()
    {
        economyScript.StartRound();
        _player.SetActive(true);
        Time.timeScale = 1;
        startTime = Time.time;
        transitionCanvas.SetActive(false);
        _camMotor.setMode(false);
        player2.GetComponent<Player2Script>().setMode(false);
    }
}
