using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyScript : MonoBehaviour
{
    //
    public Text _moneyCountTxt;
    public int money = 100;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GainMoney());
    }

    // Update is called once per frame
    void Update()
    {
        _moneyCountTxt.text = money.ToString();
    }

    private void FixedUpdate()
    {
        
    }

    IEnumerator GainMoney()
    {
        while (true)
        {
            money++;
            yield return new WaitForSeconds(1f);
        }
    }
}
