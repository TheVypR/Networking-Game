using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraMotor : NetworkBehaviour
{
    //make a singleton
    public static CameraMotor singleton;

    public Transform _playerTrans;
    public Transform _player2Trans;
    private bool isSetup = false;
    public GameObject background;

    //debug var
    public bool debug;

    //control vars
    private float moveSpeed = 1.5f;
    public float autoSpeed = 5f;
    public float followDistance = 3f;
    Vector3 Yoffset = new Vector3(0, 5, 0);
    Vector3 Xoffset = new Vector3(5, 0, 0);

    void Awake()
    {
        singleton = this;
    }

    private void OnEnable()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        //choose camera by mode
        if (PlayerPrefs.HasKey("mode"))
        {
            if (PlayerPrefs.GetInt("mode") == 2)
            {
                OnlineCamera();
            }
            else if (PlayerPrefs.GetInt("mode") == 1)
            {
                LocalCamera();
            }
            else
            {
                SingleCamera();
            }
        }             
    }

    private void OnlineCamera()
    {
        //have the server update the position
        if (isServer)
        {
            if (!debug && !isSetup)
            {
                AutoMove();
            }
            Follow();
        }

        //set the size for all machine
        if (isSetup)
        {
            Camera.main.orthographicSize = 16;
            background.transform.localScale = new Vector3(3f, 3f, 1);
        }
    }

    private void LocalCamera()
    {
        if (!debug && !isSetup)
        {
            AutoMove();
        }
        Follow();

        //set the size for all machine
        if (isSetup)
        {
            Camera.main.orthographicSize = 16;
            background.transform.localScale = new Vector3(3f, 3f, 1);
        }
    }

    private void SingleCamera()
    {
        isSetup = false;
        if (!debug && !isSetup)
        {
            AutoMove();
        }
        Follow();
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
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed*Time.deltaTime);
            if (PlayerPrefs.HasKey("mode"))
            {
                if (PlayerPrefs.GetInt("mode") == 2)
                {
                    RpcUpdateY(transform.position);
                }
            }
        }
    }

    [ClientRpc]
    void RpcUpdateY(Vector3 pos)
    {
        transform.position = pos;
    }

    void AutoMove()
    {
        transform.position += new Vector3(autoSpeed*Time.deltaTime, 0, 0);

        print("Speed: " + autoSpeed);
        print("Pos: " + transform.position);
        print("Delta: " + Time.deltaTime); 

        if (PlayerPrefs.HasKey("mode"))
        {
            if (PlayerPrefs.GetInt("mode") == 2)
            {
                RpcUpdateCamera(autoSpeed, transform.position);
            }
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
        if (!setup)
        {
            transform.position = new Vector3(0, 0, -10);
            gameObject.GetComponent<Camera>().orthographicSize = 8;
            background.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }

}
