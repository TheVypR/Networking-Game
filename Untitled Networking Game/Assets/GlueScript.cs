using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueScript : MonoBehaviour
{
    public GameObject _gluePatch;
    public Transform _patchPos;
    Rigidbody2D _rbody;
    // Start is called before the first frame update
    void Start()
    {
        int direction = Random.Range(0, 5);
        _rbody = GetComponent<Rigidbody2D>();
        if(direction == 0)
        {
            _rbody.velocity = new Vector2(0, 10);
        }
        else if(direction == 1)
        {
            _rbody.velocity = new Vector2(1, 10);
        }
        else if(direction == 2)
        {
            _rbody.velocity = new Vector2(2, 10);
        }
        else if (direction == 3)
        {
            _rbody.velocity = new Vector2(-1, 10);
        }
        else
        {
            _rbody.velocity = new Vector2(-2, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
