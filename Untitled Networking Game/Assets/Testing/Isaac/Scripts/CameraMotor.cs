using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform _playerTrans;

    //control vars
    public float moveSpeed = 0.1f;
    public float autoSpeed = 0f;
    public float followDistance = 3f;
    Vector3 offset = new Vector3(0, 5, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        AutoMove();
    }

    void Follow()
    {
        Vector3 targetPos = transform.position;

        if(_playerTrans.position.y > transform.position.y + 2)
        {
            targetPos = new Vector3(transform.position.x, _playerTrans.position.y, 0) + offset;
        } else if(_playerTrans.position.y < transform.position.y - 2)
        {
            targetPos = new Vector3(transform.position.x, _playerTrans.position.y, -10) - offset;
        }

        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, moveSpeed);
        transform.position = smoothPos;
    }

    void AutoMove()
    {
        transform.position += new Vector3(autoSpeed, 0, 0);
    }
}
