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
    public Sprite _glued;

    //check vars
    public Transform _groundCheck;
    public LvlMngrScript _mngr;
    public LayerMask _groundLayer;
    Vector2 spwn = new Vector2(0, 5);

    //control vars
    public float moveSpeed = 10;
    public float jumpSpeed = 5;
    private float waitTime = 0.15f;
    private bool playing = false;

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
    }//end Start


    private void Update()
    {
        //jump
        if (Input.GetButton("Jump") && OnGround())
        {
            y = jumpSpeed;
            _audioS.PlayOneShot(jump, (float)0.50);
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




        if ((_rBody.velocity.x > 0.2f || _rBody.velocity.x < -0.2f) && OnGround())
        {
            if (playing == false)
            {
                StartCoroutine("playFootStep");
            }

        }

        //update animation
        _anim.SetBool("Running", (x != 0));
        _anim.SetBool("Falling", (y < 0));
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal") * moveSpeed;

        if (_spriteRenderer.flipX && x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (!_spriteRenderer.flipX && x < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (transform.position.y < -10)
        {
            _mngr.PlayerDeath(spwn);
        }

    }//end FixedUpdate

    private bool OnGround()
    {

        return Physics2D.Raycast(new Vector2(_groundCheck.position.x - 0.3f, _groundCheck.position.y), Vector2.down, 0.2f, _groundLayer)
                || Physics2D.Raycast(new Vector2(_groundCheck.position.x + 0.3f, _groundCheck.position.y), Vector2.down, 0.2f, _groundLayer);
    }//end OnGround

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Trap"))
        {
            _mngr.PlayerDeath(spwn);
        }
        else if (collision.gameObject.tag.Equals("Glue"))
        {
            moveSpeed = 5;
            _anim.SetBool("Glued", true);
            StartCoroutine(GlueTime());
        }
        else if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            spwn = collision.gameObject.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Trap"))
        {
            _mngr.PlayerDeath(spwn);
        }
    }

    IEnumerator GlueTime()
    {
        yield return new WaitForSeconds(1);
        moveSpeed = 10;
        _anim.SetBool("Glued", false);
        StopCoroutine(GlueTime());
    }

    IEnumerator playFootStep()
    {
        playing = true;
        // Play the sound
        _audioS.PlayOneShot(footstep, 0.25f);
        yield return new WaitForSeconds(waitTime);
        playing = false;
    }
}