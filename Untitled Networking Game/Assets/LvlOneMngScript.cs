using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlOneMngScript : MonoBehaviour
{
    public GameObject _player;
    Transform _playerTrans;
    Rigidbody2D _playerRbody;

    Vector2 spwn;

    // Start is called before the first frame update
    void Start()
    {
        _playerRbody = _player.GetComponent<Rigidbody2D>();
        _playerTrans = _player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Respawn()
    {
        _player.SetActive(true);
        //Option 1 (Spawn Back)
        spwn = new Vector2(_playerTrans.position.x - 10, 5);

        //raycast to see if there is a platform there
        StartCoroutine(FindRespawn());
       
        _playerTrans.position = spwn;
        _playerRbody.velocity = Vector3.zero;

        //Option 2 (Spawn At Previous Checkpoint)


    }

    public void PlayerDeath()
    {
        _player.SetActive(false);
        Invoke("Respawn", 2);
    }

    IEnumerator FindRespawn()
    {
        Vector2 retryOffset = new Vector2(-1, 0);
        RaycastHit2D hit = Physics2D.Raycast(spwn, Vector2.down);
        while (!hit)
        {
            hit = Physics2D.Raycast(spwn + retryOffset, Vector2.down);
            yield return null;
        }
        if (spwn.x < -5)
        {
            spwn = new Vector2(-5, spwn.y);
        }
        StopCoroutine(FindRespawn());
    }
}
