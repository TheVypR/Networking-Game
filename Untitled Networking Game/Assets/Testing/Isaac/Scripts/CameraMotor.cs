using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform _playerTrans;
    Rigidbody2D _rbody;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTrans.position.x - gameObject.transform.position.x > 1)
        {
            _rbody.velocity = new Vector2(_playerTrans.position.x - gameObject.transform.position.x, 0);
        }
    }
}
