using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleFallingScript : MonoBehaviour
{
    public GameObject _player;
    public Transform _playerPos;

    Animator _titleAnimator;


    public bool _animStart = true;
    public float _animationTime;


    public float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _playerPos = _player.GetComponent<Transform>();
        _titleAnimator = GetComponent<Animator>();
        _animationTime = 2f;
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerPos.position.x < -6)
        {
            _animStart = false;
            _titleAnimator.SetBool("TitleFall", false);

        }
        if (!_animStart)
        {
            _animationTime -= Time.deltaTime;
        }
        if (_animationTime <= 0)
        {
            _animationTime = 0;
        }
        if (_animationTime == 0)
        {
            _animStart = true;
        }
        if (_animStart)
        {
            _titleAnimator.SetBool("TitleFall", true);
            _timer += Time.deltaTime;
        }
    }

    void KillPlayer()
    {
        _player.SetActive(false);
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
