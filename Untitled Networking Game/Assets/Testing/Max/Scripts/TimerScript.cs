using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float _lvl1Timer;
    public float _lvl2Timer;
    public float _lvl3Timer;


    public bool _lvl1 = false;
    public bool _lvl2 = false;
    public bool _lvl3 = false;


    public Text _level1;
    public Text _level2;
    public Text _level3;

    // Start is called before the first frame update
    void Start()
    {
        //2 and a half minutes
        _lvl1Timer = 150f;

        //3 and a half minutes
        _lvl2Timer = 210f;

        //5 minutes
        _lvl3Timer = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        //level 1 timer
        if (_lvl1 && !_lvl2 && !_lvl3)
        {
            _lvl1Timer -= Time.deltaTime;

            _level1.text = "" + _lvl1Timer;

            _level1.enabled = true;
            _level2.enabled = false;
            _level3.enabled = false;
        }


        //level 2 timer
        if (!_lvl1 && _lvl2 && !_lvl3)
        {
            _lvl2Timer -= Time.deltaTime;

            _level2.text = "" + _lvl2Timer;

            _level1.enabled = false;
            _level2.enabled = true;
            _level3.enabled = false;
        }


        //level 3 timer
        if (!_lvl1 && !_lvl2 && _lvl3)
        {
            _lvl3Timer -= Time.deltaTime;

            _level3.text = "" + _lvl3Timer;

            _level1.enabled = false;
            _level2.enabled = false;
            _level3.enabled = true;
        }
    }
}
