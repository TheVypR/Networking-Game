using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Mirror;

public class PlayerSelectScript : NetworkBehaviour
{
    public GameObject _highlightedP1_P1Btn;
    public GameObject _highlightedP2_P1Btn;

    public GameObject _P1selectP1Btn;
    public GameObject _P2selectP1Btn;

    public TextMeshProUGUI _P1text;
    public TextMeshProUGUI _P2text;


    public GameObject _p1Btn;
    public GameObject _p2Btn;

    public GameObject a;

    public GameObject a1;
    public GameObject b1;

    private float t1 = 1f;
    private float t2 = 1f;


    private float _highlightSizeP1 = 1;
    private float _highlightSizeP2 = 1;

    public bool _shrink;
    public bool _grow;


    ColorBlock cb_btn1;
    ColorBlock cb_btn2;

    // Start is called before the first frame update
    void Start()
    {
        /*
        cb_btn1 = _p1Btn.colors;
        cb_btn2 = _p2Btn.colors;
        */
        _shrink = false;
        _grow = true;

        if (isLocalPlayer)
        {
            _highlightedP1_P1Btn.SetActive(true);
            _highlightedP2_P1Btn.SetActive(false);

            _P1selectP1Btn.SetActive(true);
            _P2selectP1Btn.SetActive(false);

            _P1text.gameObject.SetActive(true);
            _P2text.gameObject.SetActive(false);
        }
        else
        {
            _highlightedP1_P1Btn.SetActive(false);
            _highlightedP2_P1Btn.SetActive(true);

            _P1selectP1Btn.SetActive(false);
            _P2selectP1Btn.SetActive(true);

            _P1text.gameObject.SetActive(false);
            _P2text.gameObject.SetActive(true);
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isLocalPlayer)
        {
            if (EventSystem.current.currentSelectedGameObject == _p1Btn)
            {
                //cb_btn1.selectedColor = Color.yellow;

                //Check if the posn is correctly in its place
                if (a.transform.position != a1.transform.position)
                {
                    a.transform.position = Vector2.Lerp(a.transform.position, a1.transform.position, t1);
                }



                //Floating/Breathing effect for highlighted selection
                if (_highlightedP1_P1Btn.transform.localScale.x <= 1.2f && _highlightedP1_P1Btn.transform.localScale.y <= 1.2f && _shrink == false && _grow == true)
                {
                    if (_highlightedP1_P1Btn.transform.localScale.x >= 1.19f && _highlightedP1_P1Btn.transform.localScale.y >= 1.19f)
                    {
                        _grow = false;
                        _shrink = true;
                    }
                    _highlightedP1_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 += 0.001f, _highlightSizeP1 += 0.001f, 0);
                }
                if (_highlightedP1_P1Btn.transform.localScale.x >= 1f && _highlightedP1_P1Btn.transform.localScale.y >= 1f && _grow == false && _shrink == true)
                {
                    if (_highlightedP1_P1Btn.transform.localScale.x <= 1.01f && _highlightedP1_P1Btn.transform.localScale.y <= 1.01f)
                    {
                        _grow = true;
                        _shrink = false;
                    }
                    _highlightedP1_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 -= 0.001f, _highlightSizeP1 -= 0.001f, 0);
                }
            }

            if (EventSystem.current.currentSelectedGameObject == _p2Btn)
            {
                //cb_btn2.selectedColor = Color.yellow;


                //Check if the posn is correctly in its place
                if (a.transform.position != b1.transform.position)
                {
                    a.transform.position = Vector2.Lerp(a.transform.position, b1.transform.position, t1);
                }

                //Floating/Breathing effect for highlighted selection
                if (_highlightedP1_P1Btn.transform.localScale.x <= 1.2f && _highlightedP1_P1Btn.transform.localScale.y <= 1.2f && _shrink == false && _grow == true)
                {
                    if (_highlightedP1_P1Btn.transform.localScale.x >= 1.19f && _highlightedP1_P1Btn.transform.localScale.y >= 1.19f)
                    {
                        _grow = false;
                        _shrink = true;
                    }
                    _highlightedP1_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 += 0.001f, _highlightSizeP1 += 0.001f, 0);
                }
                if (_highlightedP1_P1Btn.transform.localScale.x >= 1f && _highlightedP1_P1Btn.transform.localScale.y >= 1f && _grow == false && _shrink == true)
                {
                    if (_highlightedP1_P1Btn.transform.localScale.x <= 1.01f && _highlightedP1_P1Btn.transform.localScale.y <= 1.01f)
                    {
                        _grow = true;
                        _shrink = false;
                    }
                    _highlightedP1_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 -= 0.001f, _highlightSizeP1 -= 0.001f, 0);
                }
            }
        }
        else
        {
            if (EventSystem.current.currentSelectedGameObject == _p1Btn)
            {
                //cb_btn1.selectedColor = Color.yellow;

                //Check if the posn is correctly in its place
                if (a.transform.position != a1.transform.position)
                {
                    a.transform.position = Vector2.Lerp(a.transform.position, a1.transform.position, t1);
                }



                //Floating/Breathing effect for highlighted selection
                if (_highlightedP2_P1Btn.transform.localScale.x <= 1.2f && _highlightedP2_P1Btn.transform.localScale.y <= 1.2f && _shrink == false && _grow == true)
                {
                    if (_highlightedP2_P1Btn.transform.localScale.x >= 1.19f && _highlightedP2_P1Btn.transform.localScale.y >= 1.19f)
                    {
                        _grow = false;
                        _shrink = true;
                    }
                    _highlightedP2_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 += 0.001f, _highlightSizeP1 += 0.001f, 0);
                }
                if (_highlightedP2_P1Btn.transform.localScale.x >= 1f && _highlightedP2_P1Btn.transform.localScale.y >= 1f && _grow == false && _shrink == true)
                {
                    if (_highlightedP2_P1Btn.transform.localScale.x <= 1.01f && _highlightedP2_P1Btn.transform.localScale.y <= 1.01f)
                    {
                        _grow = true;
                        _shrink = false;
                    }
                    _highlightedP2_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 -= 0.001f, _highlightSizeP1 -= 0.001f, 0);
                }
            }

            if (EventSystem.current.currentSelectedGameObject == _p2Btn)
            {
                //cb_btn2.selectedColor = Color.yellow;


                //Check if the posn is correctly in its place
                if (a.transform.position != b1.transform.position)
                {
                    a.transform.position = Vector2.Lerp(a.transform.position, b1.transform.position, t1);
                }

                //Floating/Breathing effect for highlighted selection
                if (_highlightedP2_P1Btn.transform.localScale.x <= 1.2f && _highlightedP2_P1Btn.transform.localScale.y <= 1.2f && _shrink == false && _grow == true)
                {
                    if (_highlightedP2_P1Btn.transform.localScale.x >= 1.19f && _highlightedP2_P1Btn.transform.localScale.y >= 1.19f)
                    {
                        _grow = false;
                        _shrink = true;
                    }
                    _highlightedP2_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 += 0.001f, _highlightSizeP1 += 0.001f, 0);
                }
                if (_highlightedP2_P1Btn.transform.localScale.x >= 1f && _highlightedP2_P1Btn.transform.localScale.y >= 1f && _grow == false && _shrink == true)
                {
                    if (_highlightedP2_P1Btn.transform.localScale.x <= 1.01f && _highlightedP2_P1Btn.transform.localScale.y <= 1.01f)
                    {
                        _grow = true;
                        _shrink = false;
                    }
                    _highlightedP2_P1Btn.transform.localScale = new Vector3(_highlightSizeP1 -= 0.001f, _highlightSizeP1 -= 0.001f, 0);
                }
            }
        }
        
        
    }
}
