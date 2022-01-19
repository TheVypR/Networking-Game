using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]//require a rigidbody

public class PlayerMovementScript : MonoBehaviour
{
    //adopted vars
    Rigidbody2D _rBody;

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

        //update velocity
        //_rBody.velocity = new Vector2(x, y);
    }//end FixedUpdate

    private bool OnGround()
    {
        return Physics2D.Raycast(_groundCheck.position, Vector2.down, 0.2f);
    }//end OnGround
}