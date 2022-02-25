using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    BoxCollider2D _bCol;
    CircleCollider2D _cCol;
    Rigidbody2D _rBody;
    Animator _anim;
    Transform _trans;
    //explode sound
    AudioSource _soundSrc;
    public AudioClip explode;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _bCol = GetComponent<BoxCollider2D>();
        _cCol = GetComponent<CircleCollider2D>();
        _rBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _soundSrc = GetComponent<AudioSource>();
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
        _soundSrc.PlayOneShot(explode);
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
