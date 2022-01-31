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
    AudioSource _audioS;

    //check vars
    public Transform _groundCheck;
    public LvlMngrScript _mngr;

    //control vars
    public float moveSpeed = 10;
    public float jumpSpeed = 5;
    private float waitTime = 0.4f;
    private float startTime = 0;

    //local vars
    float x;
    float y;

    //audio vars
    public AudioClip footstep;
    public AudioClip jump;

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
        _audioS = GetComponent<AudioSource>();
        startTime = Time.deltaTime;
    }//end Start


    private void Update()
    {
        //jump
        if (Input.GetButton("Jump") && OnGround())
        {
            print("wee");
            y = jumpSpeed;
            _audioS.PlayOneShot(jump);
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

        if (_rBody.velocity.x > 0 && OnGround())
        {
            if (startTime - Time.deltaTime > waitTime)
            {
                _audioS.PlayOneShot(footstep);
            }
        }

        //update animation
        _anim.SetBool("Running", (x != 0));
        _anim.SetBool("Falling", (y < 0));
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal") * moveSpeed;

        if(_spriteRenderer.flipX && x > 0)
        {
            _spriteRenderer.flipX = false;
        } else if(!_spriteRenderer.flipX && x < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if(transform.position.y < -10)
        {
            _mngr.PlayerDeath();
        }

    }//end FixedUpdate

    private bool OnGround()
    {
        return Physics2D.Raycast(_groundCheck.position, Vector2.down, 0.2f);
    }//end OnGround

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Trap"))
        {
            _mngr.PlayerDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Trap"))
        {
            _mngr.PlayerDeath();
        }
    }
}