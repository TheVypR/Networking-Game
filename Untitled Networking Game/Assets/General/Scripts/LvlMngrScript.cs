using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlMngrScript : MonoBehaviour
{
    public GameObject _player;
    public GameObject _airStrike;
    public GameObject _lavaFlood;
    GameObject lava;

    public float levelTime;

    Transform _playerTrans;
    Rigidbody2D _playerRbody;

    Vector2 spwn;



    //Timers
    int timeLeft_lvl1;
    int timeLeft_lvl2;
    int timeLeft_lvl3;

    public float _lvl1Timer;
    public float _lvl2Timer;
    public float _lvl3Timer;

    public bool _lvl1 = false;
    public bool _lvl2 = false;
    public bool _lvl3 = false;

    public Text _level1;
    public Text _level2;
    public Text _level3;

    // Start is called before the first frame update
    void Start()
    {
        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();

        _lvl1Timer = 151f;
        _lvl2Timer = 211f;
        _lvl3Timer = 301f;
    }

    // Update is called once per frame
    void Update()
    {
        //Level 1 Timer
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TestScene"))
        {
            _lvl1 = true;
            _lvl2 = false;
            _lvl3 = false;
            if (_lvl1)
            {
                //update timer
                timeLeft_lvl1 = (int)(levelTime - (Time.time - _lvl1Timer));

                //update timer text
                _level1.text = "Time: " + timeLeft_lvl1;

                if (timeLeft_lvl1 <= 0)
                {
                    //game over
                    //Destroy(_player);
                    //Invoke("EndGame", 2);
                }
            }
        }
            

        //Level 2 Timer
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 2"))
        {
            _lvl1 = false;
            _lvl2 = true;
            _lvl3 = false;
            if (_lvl2)
            {
                //update timer
                timeLeft_lvl2 = (int)(levelTime - (Time.time - _lvl2Timer));

                //update timer text
                _level2.text = "Time: " + timeLeft_lvl2;

                if (timeLeft_lvl2 <= 0)
                {
                    //game over
                    //Destroy(_player);
                    //Invoke("EndGame", 2);
                }
            }
        }


        //Level 3 Timer
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 3"))
        {
            _lvl1 = false;
            _lvl2 = false;
            _lvl3 = true;
            if (_lvl3)
            {
                //update timer
                timeLeft_lvl3 = (int)(levelTime - (Time.time - _lvl3Timer));

                //update timer text
                _level3.text = "Time: " + timeLeft_lvl3;

                if (timeLeft_lvl3 <= 0)
                {
                    //game over
                    //Destroy(_player);
                    //Invoke("EndGame", 2);
                }
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
        _player.SetActive(true);
        //Option 1 (Spawn Back)
        //spwn = new Vector2(_playerTrans.position.x - 25, 10);

        //raycast to see if there is a platform there
        //StartCoroutine(FindRespawn());

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
        Invoke("Respawn", 2);
    }

    IEnumerator FindRespawn()
    {
        Vector2 retryOffset = new Vector2(-1, 0);
        RaycastHit2D hit = Physics2D.Raycast(spwn, Vector2.down);
        while (!hit)
        {
            print("Find");
            spwn += retryOffset;
            hit = Physics2D.Raycast(spwn, Vector2.down);
            if (spwn.x < -5)
            {
                spwn = new Vector2(-5, spwn.y);
                break;
            }
            yield return null;
            
        }
        
        StopCoroutine(FindRespawn());
    }

    void EndGame()
    {

    }
}
