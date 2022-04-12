using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraMotor : NetworkBehaviour
{
    public Transform _playerTrans;
    public Transform _player2Trans;
    private bool isSetup = false;
    public GameObject background;

    //debug var
    public bool debug;

    //control vars
    private float moveSpeed = 1.5f;
    public float autoSpeed = 35f;
    public float followDistance = 3f;
    Vector3 Yoffset = new Vector3(0, 5, 0);
    Vector3 Xoffset = new Vector3(5, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (!debug && !isSetup)
            {
                AutoMove();
            }
            if (isSetup)
            {
                Camera.main.orthographicSize = 16;
                background.transform.localScale = new Vector3(3f, 3f, 1);
            }
            Follow();
        }
    }

    void Follow()
    {
        Vector3 targetPos = transform.position;
        if (_playerTrans)
        {
            if (!isSetup)
            {
                if (debug)
                {
                    if (_playerTrans.position.y > transform.position.y + 2)
                    {
                        targetPos = new Vector3(transform.position.x, _playerTrans.position.y, -10) + Yoffset;
                    }
                    else if (_playerTrans.position.y < transform.position.y - 2)
                    {
                        targetPos = new Vector3(transform.position.x, _playerTrans.position.y, -10) - Yoffset;
                    }
                    else if (_playerTrans.position.x > transform.position.x + 3)
                    {
                        targetPos = new Vector3(_playerTrans.position.x, transform.position.y, -10) + Xoffset;
                    }
                    else if (_playerTrans.position.x < transform.position.x - 3)
                    {
                        targetPos = new Vector3(_playerTrans.position.x, transform.position.y, -10) - Xoffset;
                    }
                }
                else
                {
                    if (_playerTrans.position.y > transform.position.y + 0.2)
                    {
                        targetPos = new Vector3(transform.position.x, _playerTrans.position.y, -10) + Yoffset;
                    }
                    else if (_playerTrans.position.y < transform.position.y - 0.5)
                    {
                        targetPos = new Vector3(transform.position.x, _playerTrans.position.y, -10) - Yoffset;
                    }                
                }
            } else
            {
                targetPos = new Vector3(_player2Trans.position.x, _player2Trans.position.y, -10);
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    void AutoMove()
    {
        if (isServer)
        {
            transform.position += new Vector3(autoSpeed*Time.deltaTime, 0, 0);
            RpcUpdateCamera(autoSpeed, transform.position);
        }
    }

    [ClientRpc]
    void RpcUpdateCamera(float curSpeed, Vector3 pos)
    {
        transform.position = pos;
        autoSpeed = curSpeed;
    }

    public void setMode(bool setup)
    {
        isSetup = setup;
        if (!isSetup)
        {
            Camera.main.orthographicSize = 8;
            background.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
