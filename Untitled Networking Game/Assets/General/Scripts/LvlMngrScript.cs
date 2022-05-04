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
    //check mode
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
    public TMP_Text _winScreen;
    public TMP_Text _loseScreen;

    //setup or gameplay
    public GameObject transitionCanvas;
    Transform _camTrans;
    bool isSetup = false;
    public float setupTimer = 30;
    float setupStart;
    public AudioSource audioS;

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
    void OnEnable()
    {
        //set up respawn screen
        _timeRespawn = 4f;
        _countDeaths = 0;
        _textRise = 0;

        //respawn canvas children
        _respawnText = _respawnCanvas.transform.Find("RespawnCountdownText").gameObject.GetComponent<Text>();
        _respawnTextBack = _respawnCanvas.transform.Find("RespawnCountdownText (1)").gameObject.GetComponent<Text>();
        _respawning = _respawnCanvas.transform.Find("RespawningText").gameObject.GetComponent<TMP_Text>();
        _deathText = _respawnCanvas.transform.Find("Death Count").gameObject.GetComponent<TMP_Text>();
        _addPlusOne = _respawnCanvas.transform.Find("AddOneDeathText").gameObject.GetComponent<Text>();
        _respawnCanvas.GetComponent<Canvas>().enabled = false;
        transitionCanvas.GetComponent<Canvas>().enabled = false;

        //check if multiplayer
        if (PlayerPrefs.HasKey("mode"))
        {
            isMultiplayer = PlayerPrefs.GetInt("mode");
            if(isMultiplayer == 2)
            {
                audioS.Play();
                StartOnlineMultiplayer();
            } else if (isMultiplayer == 1)
            {
                audioS.Play();
                StartLocalMultiplayer();
            } else
            {
                audioS.Play();
                StartSinglePlayer();
            }
        } else
        {
            PlayerPrefs.SetInt("mode", 0);
            isMultiplayer = 0;
            CameraMotor.singleton.setMode(false);
            singleplayerAI.SetActive(true);
        }

        //store components
        _camTrans = CameraMotor.singleton.transform;
        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _playerMove = _player.GetComponent<PlayerMovementScript>();
    }



    private IEnumerator SearchPlayers()
    {
        while (FindObjectsOfType<PlayerManagerScript>().Length > 1)
        {
            yield return new WaitForFixedUpdate();
        }
        NetworkManager.singleton.StopHost();
    }

    public void StartSetup()
    {
        isSetup = true;
        setupStart = Time.time;
    }

    void StartSinglePlayer()
    {
        //start in play mode
        isSetup = false;
        startTime = Time.time;

        //disable player 2
        player2.GetComponent<Player2Script>().enabled = false;
        player2.GetComponentInChildren<SpriteRenderer>().enabled = false;

        //start camera in game mode
        CameraMotor.singleton.setMode(false);

        //disable the respawn screen
        _respawnCanvas.SetActive(false);
        transitionCanvas.SetActive(false);
    }

    public void StartLocalMultiplayer()
    {
        //start in setup mode
        isSetup = true;
        setupStart = Time.time;

        //disable trap AI
        singleplayerAI.SetActive(false);
        singleplayerAI.GetComponent<TrapAIScript>().enabled = false;

        //guarantee p2 is active
        player2.SetActive(true);

        //disable p1 script for setup phase
        _player.GetComponent<PlayerMovementScript>().enabled = false;

        CameraMotor.singleton.setMode(true);
    }

    void StartOnlineMultiplayer()
    {
        //start in setup mode
        isSetup = true;
        setupStart = Time.time;

        //disable trap AI
        singleplayerAI.SetActive(false);
        singleplayerAI.GetComponent<TrapAIScript>().enabled = false;

        //guarantee p2 is active
        player2.SetActive(true);

        //disable p1 script for setup phase
        _player.GetComponent<PlayerMovementScript>().enabled = false;

        //turn the camera on the right mode
        CameraMotor.singleton.setMode(true);

        //keep an eye on the number of players to check for disconnection
        StartCoroutine(SearchPlayers());
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
        if (isMultiplayer == 2)
        {
            CmdStartRound();
        } else
        {
            //reset camera
            economyScript.StartRound();
            isSetup = false;
            CameraMotor.singleton.setMode(false);
            _player.GetComponent<PlayerMovementScript>().enabled = true;
            Time.timeScale = 1;
            startTime = Time.time;
            transitionCanvas.SetActive(false);
            player2.GetComponent<Player2Script>().setMode(false);
        }
    }

    [Command (requiresAuthority=false)]
    void CmdStartRound()
    {
        //reset camera on server
        economyScript.StartRound();
        isSetup = false;
        CameraMotor.singleton.setMode(false);
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
        CameraMotor.singleton.setMode(false);
        _player.GetComponent<PlayerMovementScript>().enabled = true;
        Time.timeScale = 1;
        startTime = Time.time;
        transitionCanvas.SetActive(false);
        
        player2.GetComponent<Player2Script>().setMode(false);
    }
}
