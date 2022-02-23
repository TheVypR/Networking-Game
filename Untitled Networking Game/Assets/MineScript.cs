using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Boom?");
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("Boom");
            _anim.SetBool("Triggered", true);
        }
    }

    private void ResetMine()
    {
        _anim.SetBool("Triggered", false);
    }
}
