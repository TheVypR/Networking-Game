using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningMMScript : MonoBehaviour
{
    public Rigidbody2D _player;
    Animator _playerAnimator;

    public GameObject _floor;

    public bool _stop = false;
    private float _playerStop;


    public bool _startAnimation = false;
    public float _animationTime;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.x < 0)
        {
            _player.velocity = new Vector2(3, 0);
            _playerAnimator.SetBool("Running", true);
        }

        //check if the player is at x = -2 and start the animation
        if (_player.transform.position.x > -2)
        {
            _startAnimation = true;
            _animationTime = 5f;
            if (_startAnimation)
            {
                _playerAnimator.SetBool("Running", false);
                _animationTime -= Time.deltaTime;
            }
            if (_animationTime <= 0)
            {
                _animationTime = 0;
            }
            if (_animationTime == 0)
            {
                _startAnimation = false;
            }
        }
    }

    public void SquashPlayer()
    {
        Destroy(gameObject);
    }

    
}
