using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : TrapScript
{
    Animator _anim;

    //explode sound
    AudioSource _src;
    public AudioClip explode;

    public int charge = 125;
    public override int cost { get { return charge; } set { charge = value; } }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _src = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Boom?");
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("Boom");
            _anim.SetBool("Triggered", true);
            _src.PlayOneShot(explode);
        }
    }

    private void ResetMine()
    {
        _anim.SetBool("Triggered", false);
    }
}
