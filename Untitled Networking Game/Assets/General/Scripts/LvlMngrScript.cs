using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlMngrScript : MonoBehaviour
{
    public GameObject _player;
    public GameObject _airStrike;
    public GameObject _lavaFlood;
    GameObject lava;

    public float levelTime;
    float startTime;
    int timeLeft;
    public Text _timer;

    Transform _playerTrans;
    Rigidbody2D _playerRbody;

    Vector2 spwn;

    // Start is called before the first frame update
    void Start()
    {
        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //update timer
        timeLeft = (int) (levelTime - (Time.time - startTime));

        //update timer text
        _timer.text = "Time: " + timeLeft;

        if(timeLeft <= 0)
        {
            //game over
            //Destroy(_player);
            //Invoke("EndGame", 2);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(_airStrike);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            lava = Instantiate(_lavaFlood, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 8, 0), Quaternion.identity);
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
