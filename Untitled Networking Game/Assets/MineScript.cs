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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _anim.SetBool("Triggered", true);
            Invoke("ResetMine", 2);
        }
    }

    private void ResetMine()
    {
        _anim.SetBool("Triggered", false);
    }
}
