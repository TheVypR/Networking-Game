using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform _playerTrans;
    public Transform _player2Trans;
    private bool isP2 = false;

    //debug var
    public bool debug;

    //control vars
    private float moveSpeed = 1.5f;
    public float autoSpeed = 0.035f;
    public float followDistance = 3f;
    Vector3 Yoffset = new Vector3(0, 5, 0);
    Vector3 Xoffset = new Vector3(5, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!debug && !isP2)
        {
            AutoMove();
        }
        if (isP2)
        {
            p2();
        }
        Follow(debug);

    }

    void Follow(bool isDebug)
    {
        Vector3 targetPos = transform.position;
        if (_playerTrans)
        {
            if (!isP2)
            {
                if (isDebug)
                {
                    if (_playerTrans.position.y > transform.position.y + 2)
                    {
                        targetPos = new Vector3(transform.position.x, _playerTrans.position.y, 0) + Yoffset;
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
                        targetPos = new Vector3(transform.position.x, _playerTrans.position.y, 0) + Yoffset;
                    }
                    else if (_playerTrans.position.y < transform.position.y - 0.5)
                    {
                        targetPos = new Vector3(transform.position.x, _playerTrans.position.y, -10) - Yoffset;
                    }                
                }
            } else
            {
                targetPos = new Vector3(_playerTrans.position.x, _playerTrans.position.y, 10);
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    void AutoMove()
    {
        transform.position += new Vector3(autoSpeed, 0, 0);
    }

    void setMode(bool p2)
    {
        isP2 = p2;
    }

    void p2()
    {
        transform.position = Vector3.Lerp(transform.position, _player2Trans.position, moveSpeed * Time.deltaTime);
    }
}
