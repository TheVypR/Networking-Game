using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Mirror;

public class LvlMngrScript : NetworkBehaviour
{
    //check multiplier
    int isMultiplayer;
    public GameObject player2;
    public GameObject singleplayerAI;
    public EconomyScript economyScript;

    //player objects
    public GameObject _player;
    public SpriteRenderer _playerSprite;
    public PlayerMovementScript _playerMove;
    Transform _playerTrans;
    Rigidbody2D _playerRbody;
    public GameObject _setupCanvas;

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
    Transform _camTrans;
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
        //SceneManager.UnloadSceneAsync("LevelSelect");
        //check if multiplayer
        if (PlayerPrefs.HasKey("mode"))
        {
            PlayerPrefs.SetInt("mode", 2);
            isMultiplayer = 2;//PlayerPrefs.GetInt("mode");
            if(isMultiplayer == 2)
            {
                singleplayerAI.SetActive(false);
                _player.GetComponent<PlayerMovementScript>().enabled = false;
                player2.SetActive(true);
                _camMotor.setMode(true);
            } else if (isMultiplayer == 1)
            {
                singleplayerAI.SetActive(false);
                _player.GetComponent<PlayerMovementScript>().enabled = false;
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

        _camTrans = Camera.main.transform;
        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _playerMove = _player.GetComponent<PlayerMovementScript>();

        //only start round if singleplayer
        if (isMultiplayer == 0) { startTime = Time.time; }

        _timeRespawn = 4f;
        _countDeaths = 0;
        _textRise = 0;

        if (isMultiplayer == 1 || isMultiplayer == 2)
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
        _respawnCanvas.SetActive(false);
        transitionCanvas.SetActive(false);
    }

    void StartSinglePlayer()
    {
        //start in play mode
        startTime = Time.time;
        //disable trap AI
        player2.SetActive(false);

    }

    void StartLocalMultiplayer()
    {
        //start in setup mode
        isSetup = true;
        setupStart = Time.time;

        //disable trap AI
        singleplayerAI.SetActive(false);

        //guarantee p2 is active
        player2.SetActive(true);

        //disable p1 script for setup phase
        _player.GetComponent<PlayerMovementScript>().enabled = false;
    }

    void StartOnlineMultiplayer()
    {
        //start in setup mode
        isSetup = true;
        setupStart = Time.time;

        //disable trap AI
        singleplayerAI.SetActive(false);

        //guarantee p2 is active
        player2.SetActive(true);

        //disable p1 script for setup phase
        _player.GetComponent<PlayerMovementScript>().enabled = false;
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
            if ((isMultiplayer == 1 || isMultiplayer == 2) && timeLeft <= 0)
            {
                //load intermediary canvas and pause time
                Time.timeScale = 0;
                transitionCanvas.SetActive(true);
                _setupCanvas.SetActive(false);
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
        _respawnCanvas.SetActive(false);
        _timeRespawn = 4f;
        _dead = false;
        _textRise = 0;

        //_player.SetActive(true);
        _playerSprite.enabled = true;
        _playerMove.enabled = true;

        _playerTrans.position = spwn;
        Vector3 camPos = new Vector3(spwn.x, spwn.y, -10);
        _camTrans.position = camPos;
        if (isServer)
        {
            RpcCameraReset(camPos);
        }
        if (isServer)
        {
            RpcPlayerReset(spwn);
        }
        _playerRbody.velocity = Vector3.zero;
        _player.GetComponent<PlayerMovementScript>().moveSpeed = 10;
    }

    [Command]
    void CmdCameraReset(Vector3 pos)
    {
        _camTrans.position = pos;
        RpcCameraReset(pos);
    }

    [ClientRpc]
    void RpcCameraReset(Vector3 pos)
    {
        _camTrans.position = pos;
    }

    [ClientRpc]
    void RpcPlayerReset(Vector3 pos)
    {
        _playerTrans.position = pos;
    }

    public void PlayerDeath(Vector2 spwn)
    {
        print(spwn);
        this.spwn = spwn;
        //_player.SetActive(false);
        _playerSprite.enabled = false;
        _playerMove.enabled = false;
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
        CmdStartRound();
    }

    [Command (requiresAuthority=false)]
    void CmdStartRound()
    {
        //reset camera on server
        economyScript.StartRound();
        isSetup = false;
        _camMotor.setMode(false);
        _player.GetComponent<PlayerMovementScript>().enabled = true;
        Time.timeScale = 1;
        startTime = Time.time;
        transitionCanvas.SetActive(false);
        player2.GetComponent<Player2Script>().setMode(false);

        //reset camera on all clients
        ClientStartRound();
    }

    [ClientRpc]
    void ClientStartRound()
    {
        economyScript.StartRound();
        isSetup = false;
        _camMotor.setMode(false);
        _player.GetComponent<PlayerMovementScript>().enabled = true;
        Time.timeScale = 1;
        startTime = Time.time;
        transitionCanvas.SetActive(false);
        
        player2.GetComponent<Player2Script>().setMode(false);
    }
}
