using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]//require a rigidbody

public class PlayerMovementScript : MonoBehaviour
{
    //adopted vars
    Rigidbody2D _rBody;
    SpriteRenderer _spriteRenderer;
    Animator _anim;

    //check vars
    public Transform _groundCheck;

    //control vars
    public float moveSpeed = 10;
    public float jumpSpeed = 5;
    public float airSpeed = 3;

    //local vars
    float x;
    float y;

    enum STATE
    {
        WALKING = 0,
        JUMPING = 1
    }//state enum
    STATE curState = STATE.WALKING;

    private void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }//end Start

    private void Update()
    {
        //jump
        if (Input.GetButton("Jump") && OnGround())
        {
            print("wee");
            y = jumpSpeed;
        }
        else if (Input.GetButtonUp("Jump") && _rBody.velocity.y > 0)
        {
            y = 0;
        }
        else
        {
            y = _rBody.velocity.y;
        }//end if/elseif/else

        //update velocity
        _rBody.velocity = new Vector2(x, y);

        //update animation
        _anim.SetBool("Running", (x != 0));
        _anim.SetBool("Falling", (y < 0));
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");

        //horizontal movement
        if (OnGround())
        {
            print("gruonded");
            x *= moveSpeed;
        }
        else
        {
            x *= airSpeed;
        }//end if/else

        if(_spriteRenderer.flipX && x > 0)
        {
            _spriteRenderer.flipX = false;
        } else if(!_spriteRenderer.flipX && x < 0)
        {
            _spriteRenderer.flipX = true;
        }

    }//end FixedUpdate

    private bool OnGround()
    {
        return Physics2D.Raycast(_groundCheck.position, Vector2.down, 0.2f);
    }//end OnGround
}