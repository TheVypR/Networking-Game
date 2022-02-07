using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleFallingScript : MonoBehaviour
{
    public Transform _playerPos;
    public OpeningMMScript _player;

    Animator _titleAnimator;


    public bool _animStart = true;
    public float _animationTime;


    public float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _playerPos = GetComponent<Transform>();
        _titleAnimator = GetComponent<Animator>();
        _player = GetComponent<OpeningMMScript>();
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
        if (_timer >= 2f)
        {
            
            Invoke("LoadMainMenuScene", 1);
        }
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
