using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManagerScript : MonoBehaviour
{
    CameraMotor _camScript;
    // Start is called before the first frame update
    void Start()
    {
        _camScript = FindObjectOfType<CameraMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Spend Coins
    void SpendCoin(int amt)
    {

    }

    IEnumerator DrainCoin()
    {
        while (true)
        {

        }
    }
}
