using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class EconomyScript : NetworkBehaviour
{
    //Camera control
    CameraMotor _camScript;
    public const float MAX_SPEED = 0.065f;
    public const float MIN_SPEED = 0f;
    public NetworkIdentity _player1ID;

    //control vars
    public Text _moneyCountTxt;
    public int money = 100;
    int drainAmt = 0;
    bool setupMode = false;

    //money control
    Coroutine gainRoutine;
    Coroutine drainRoutine;




    //Increase Gains if not spent variables
    float _timeNotSpent;

    //public CanvasManagerScript canvScript;

    // Start is called before the first frame update
    void Start()
    {
        _camScript = Resources.FindObjectsOfTypeAll<CameraMotor>()[0];
        setupMode = (PlayerPrefs.GetInt("mode") == 1 || PlayerPrefs.GetInt("mode") == 2);
        if (!setupMode)
        {
            gainRoutine = StartCoroutine(GainMoney());
            _timeNotSpent = Time.time;
        }
    }

    public void StartRound()
    {
        setupMode = false;
        //canvScript.setPlayerOneBlind(false);
        gainRoutine = StartCoroutine(GainMoney());
        if (_player1ID.hasAuthority)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _moneyCountTxt.text = money.ToString();
        
        //check if the player has money to spend
        if(money <= 0 && !setupMode)
        {
            NoMoney();
        }
    }

    void NoMoney()
    {
        money = 0;
        CameraMotor.singleton.autoSpeed = 5f;
        StopAllCoroutines();
        gainRoutine = StartCoroutine(GainMoney());
    }


    public void stopCamSpeed()
    {
        print("stop");
        CameraMotor.singleton.autoSpeed = 5f;
        StopAllCoroutines();
        gainRoutine = StartCoroutine(GainMoney());
    }

    public void CameraSpeed(bool fastSlow)
    {
        if (fastSlow)
        {
            CameraMotor.singleton.autoSpeed += 0.02f;
            drainAmt = (int)(Mathf.Abs((CameraMotor.singleton.autoSpeed - 0.035f) * 500));
            if (drainAmt == 0 && drainRoutine != null)
            {
                gainRoutine = StartCoroutine(GainMoney());
                StopCoroutine(drainRoutine);
            }
            else
            {
                StopCoroutine(gainRoutine);
                drainRoutine = StartCoroutine(DrainMoney(drainAmt));
            }
        }
        else
        {
            CameraMotor.singleton.autoSpeed -= 0.02f;
            drainAmt = (int)(Mathf.Abs((_camScript.autoSpeed - 5f + 0.01f) * 100));
            if (drainAmt == 0 && drainRoutine != null)
            {
                gainRoutine = StartCoroutine(GainMoney());
                StopCoroutine(drainRoutine);
            }
            else
            {
                StopCoroutine(gainRoutine);
                drainRoutine = StartCoroutine(DrainMoney(drainAmt));
            }
        }
    }

    public bool SpendCoin(int amt)
    {
        if (money - amt < 0)
        {
            return false;
        }
        else
        {
            money -= amt;
            _timeNotSpent = Time.time;
            return true;
        }
    }

    IEnumerator GainMoney()
    {
        while (true)
        {
            if (Time.time - _timeNotSpent > 7f)
            {
                money += 2;
            }
            if (Time.time - _timeNotSpent > 14f)
            {
                money += 5;
            }
            money++;
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator DrainMoney(int amt)
    {
        while(true)
        {
            money -= amt;
            yield return new WaitForSeconds(0.75f);
        }
    }
}
