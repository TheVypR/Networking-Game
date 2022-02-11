using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    BoxCollider2D _bCol;
    CircleCollider2D _cCol;
    SpriteRenderer _rend;
    Rigidbody2D _rBody;
    Animator _anim;
    Transform _trans;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _bCol = GetComponent<BoxCollider2D>();
        _cCol = GetComponent<CircleCollider2D>();
        _rend = GetComponent<SpriteRenderer>();
        _rBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_trans.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bCol.enabled = true;
        _cCol.enabled = false;
        _rBody.gravityScale = 0;
        _anim.Play("Explosion");
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
