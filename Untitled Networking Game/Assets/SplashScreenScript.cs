using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    public GameObject _monkeLogo;


    public AudioClip mm_monke;
    AudioSource audio;


    public bool _playAudio = false;
    public bool _audioTemp = false;

    private float colorClone;



    private float _openSplash;
    private float _closeSplash;
    public bool _runSplash = false;
    public bool _splashFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();


        Color _colorM = _monkeLogo.GetComponent<SpriteRenderer>().color;
        _colorM.a = 0f;
        _monkeLogo.GetComponent<SpriteRenderer>().color = _colorM;

        _openSplash = 4f;
        _closeSplash = 0.9f;
        _runSplash = true;

        colorClone = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float x = _monkeLogo.transform.localScale.x;
        float y = _monkeLogo.transform.localScale.y;
        //Scale up image over time
        _monkeLogo.transform.localScale = new Vector2(x += 0.0003f, y += 0.0003f);





        //Make Transparent
        Color _colorM = _monkeLogo.GetComponent<SpriteRenderer>().color;
        if (_runSplash)
        {

            //Increase the value of variable so that we can get enable the play of audio
            if (!_playAudio && !_audioTemp)
            {
                colorClone += 0.02f;
            }


            if (_openSplash > 0)
            {
                _openSplash -= Time.deltaTime;
                _colorM.a += 0.02f;
            }
            else
            {
                _openSplash = 0f;
                _colorM.a = 1f;
            }
            if (_openSplash == 0)
            {
                _runSplash = false;
            }
        }


        //Handles the audio timer
        //This set up allows the audio to only play one time
        //if the value is above 2, set color back to 0, enable both booleans
        if (colorClone > 1.5f)
        {
            colorClone = 0;
            _playAudio = true;
            _audioTemp = true;
        }

        //if the audio boolean is true, call invoke, set boolean back to false
        if (_playAudio)
        {
            Invoke("PlayAudioFile", 0.5f);
            _playAudio = false;
        }

        //Make Opaque
        if (!_runSplash)
        {
            if (_closeSplash > 0)
            {
                _closeSplash -= Time.deltaTime;
                _colorM.a -= 0.025f;
            }
            else
            {
                _closeSplash = 0f;
                _colorM.a = 0f;
            }
            if (_closeSplash == 0)
            {
                _runSplash = false;

                //if finished, run the invoke to load next scene
                if (_colorM.a == 0f)
                {
                    Invoke("RunNextScene", 0.5f);
                }
            }
        }
        _monkeLogo.GetComponent<SpriteRenderer>().color = _colorM;
    }

    void RunNextScene()
    {
        SceneManager.LoadScene("OpeningScene");
    }



    void PlayAudioFile()
    {
        audio.PlayOneShot(mm_monke);
    }
}
