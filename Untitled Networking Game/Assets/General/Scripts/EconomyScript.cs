using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyScript : MonoBehaviour
{
    //Camera control
    CameraMotor _camScript;
    public const float MAX_SPEED = 0.065f;
    public const float MIN_SPEED = 0f;

    //control vars
    public Text _moneyCountTxt;
    public int money = 100;
    int drainAmt = 0;
    bool setupMode = false;

    //money control
    Coroutine gainRoutine;
    Coroutine drainRoutine;

    // Start is called before the first frame update
    void Start()
    {
        _camScript = FindObjectOfType<CameraMotor>();
        setupMode = PlayerPrefs.GetInt("mode") == 1;
        if (!setupMode)
        {
            gainRoutine = StartCoroutine(GainMoney());
        }
    }

    public void StartGain()
    {
        gainRoutine = StartCoroutine(GainMoney());
    }

    // Update is called once per frame
    void Update()
    {
        _moneyCountTxt.text = money.ToString();
        

        if(money <= 0)
        {
            NoMoney();
        }
    }

    void NoMoney()
    {
        money = 0;
        _camScript.autoSpeed = 0.035f;
        StopAllCoroutines();
        gainRoutine = StartCoroutine(GainMoney());
    }


    public void stopCamSpeed()
    {

        _camScript.autoSpeed = 0.035f;
        StopAllCoroutines();
        gainRoutine = StartCoroutine(GainMoney());
    }

    public void CameraSpeed(int fastSlow)
    {
        if (fastSlow == 0)
        {
            _camScript.autoSpeed += 0.01f;
            drainAmt = (int)(Mathf.Abs((_camScript.autoSpeed - 0.035f) * 100));
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
        else if (fastSlow == 1)
        {
            _camScript.autoSpeed -= 0.01f;
            drainAmt = (int)(Mathf.Abs((_camScript.autoSpeed - 0.035f) * 100));
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

    public void SpendCoin(int amt)
    {
        money -= amt;
    }

    IEnumerator GainMoney()
    {
        while (true)
        {
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
