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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_playerTrans.position.y > transform.position.y + followDistance)
        {
            transform.position += new Vector3(0, moveSpeed, 0);
        } else if(_playerTrans.position.y < transform.position.y - followDistance)
        {
            transform.position -= new Vector3(0, moveSpeed, 0);
        }
    }
}
