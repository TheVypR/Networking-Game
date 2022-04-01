using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]//require a rigidbody
public class PlayerMovementScript : PlayerBaseScript
{
    //adopted vars
    Rigidbody2D _rBody;
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    AudioSource _audioS;
    Transform _cam;
    Transform _trans;
    public Sprite _glued;

    //check vars
    public Transform _groundCheck;
    public LvlMngrScript _mngr;
    public LayerMask _groundLayer;
    Vector2 spwn = new Vector2(0, 5);

    //control vars
    public float moveSpeed = 2;
    public float jumpSpeed = 5;

    //local vars
    float x;
    float y;

    //audio vars
    public AudioClip footstep;
    public AudioClip jump;
    public AudioClip lavaDeath;
    public AudioClip fallOutDeath;

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
        _cam = Camera.main.transform;
        _trans = GetComponent<Transform>();
        if (!_mngr)
        {
            _mngr = FindObjectOfType<LvlMngrScript>();
        }
    }//end Start


    private void Update()
    {
        //jump
        if (MyInput.GetKeyInteract(2) && OnGround())
        {
            y = jumpSpeed;
            _audioS.PlayOneShot(jump, (float) 0.50);
        }
        else if (!MyInput.GetKeyInteract(2) && _rBody.velocity.y > 0)
        {
            y = 0;
        }
        else
        {
            y = _rBody.velocity.y;
        }//end if/elseif/else

        //update velocity
        //_rBody.velocity = new Vector2(x, y);

        //update animation
        _anim.SetBool("Running", (x != 0));
        _anim.SetBool("Falling", (y < -0.001));
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.HasKey("player")) {
            print("has key");
            if (PlayerPrefs.GetInt("player") == 1)
            {
                x = MyInput.GetXAxis(2) * moveSpeed;
                print(x);
                StepMovement(x, y);
                if (!isServer)
                {
                    print("client");
                    CmdUpdatePosn(new Vector2(x, y), _trans.position);
                }
                else
                {
                    print("server");
                    RpcUpdatePosn(_trans.position);
                }
            }

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
                _audioS.PlayOneShot(fallOutDeath, 0.5f);
                _mngr.PlayerDeath(spwn);
            }
            else if (transform.position.x < _cam.position.x - 16)
            {
                _mngr.PlayerDeath(spwn);
            }
        }
    }//end FixedUpdate

    void StepMovement(float x, float y)
    {
        Vector2 axes = new Vector2(x, y);
        Vector2 delta = axes * moveSpeed * Time.fixedDeltaTime;
        _trans.Translate(delta);
    }

    private bool OnGround()
    {
        bool grounded = Physics2D.Raycast(new Vector2(_groundCheck.position.x - 0.25f, _groundCheck.position.y), Vector2.down, 0.15f, _groundLayer)
                || Physics2D.Raycast(new Vector2(_groundCheck.position.x + 0.25f, _groundCheck.position.y), Vector2.down, 0.15f, _groundLayer);
        return grounded;
    }//end OnGround

    [Command]
    void CmdUpdatePosn(Vector2 axes, Vector2 posn)
    {
        print("cmd");
        StepMovement(axes.x, axes.y);
        if (Vector2.Distance(_trans.position, posn) > 0.1f)
        {
            TargetPosnError(connectionToClient, _trans.position);
        }
        RpcUpdatePosn(_trans.position);
    }

    [ClientRpc]
    void RpcUpdatePosn(Vector2 posn)
    {
        if (!isLocalPlayer)
        {
            print("rpc");
            if (_trans == null)
                return;

            _trans.position = posn;
        }
    }

    [TargetRpc]
    void TargetPosnError(NetworkConnection conn, Vector2 posn)
    {
        print("target");
        transform.position = posn;
    }

    //fires if the player runs into a trigger
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
            Invoke("PlayerDeath", 1);
        } else if (collision.gameObject.tag.Equals("Lava"))
        {
            _anim.SetBool("LavaContact", true);
            _audioS.PlayOneShot(lavaDeath);
            Invoke("PlayerDeath", 0.5f);
        }
    }

    void PlayerDeath()
    {
        _anim.SetBool("LavaContact", false);
        _anim.SetBool("Running", false);
        _anim.SetBool("Falling", false);
        _anim.SetBool("Glued", false);
        _mngr.PlayerDeath(spwn);
    }

    IEnumerator GlueTime()
    {
        yield return new WaitForSeconds(2);
        moveSpeed = 10;
        _anim.SetBool("Glued", false);
        StopCoroutine(GlueTime());
    }
}