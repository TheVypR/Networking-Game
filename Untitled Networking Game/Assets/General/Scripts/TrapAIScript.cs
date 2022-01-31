using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAIScript : MonoBehaviour
{

    public GameObject spawners;
    public GameObject mine;
    System.Random rando = new System.Random();
    BombStrikeScript bombScript;
    LavaScript lavaScript;
    EconomyScript econScript;
    private readonly int MAX_TRAP_COST = 75;
    public GameObject bombSPrefab;
    public GameObject lavaPrefab;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (econScript.money > MAX_TRAP_COST)
        {
            int num = rando.Next(0, 5000);

            if (num < 50)
            {
                Instantiate(bombSPrefab);
                econScript.SpendCoin(bombScript._cost);
            }
            else if (num < 10)
            {
                Instantiate(lavaPrefab);
                econScript.SpendCoin(lavaScript.cost);
            }
        }
    }
}
