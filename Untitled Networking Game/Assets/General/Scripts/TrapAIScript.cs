using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrapAIScript : MonoBehaviour
{

    System.Random rando = new System.Random();

    public GameObject spawners;
    public GameObject mine;
    public GameObject bombSPrefab;
    public GameObject lavaPrefab;
    public GameObject blindPrefab;

    BombStrikeScript bombStrikeScript;
    LavaScript lavaScript;
    BlindScript blindScript;
    EconomyScript econScript;

    private int MAX_TRAP_COST;




    // Start is called before the first frame update
    void Start()
    {
        int[] prices = { bombStrikeScript._cost, lavaScript._cost, blindScript._cost };
        MAX_TRAP_COST = prices[prices.Max()];
        print("Max Trap Cost: " + MAX_TRAP_COST);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (econScript.money > MAX_TRAP_COST)
        {
            int num = rando.Next(0, 5000);

            if (num < 5)
            {
                Instantiate(blindPrefab);
                econScript.SpendCoin(blindScript._cost);
            }
            else if (num < 20)
            {
                Instantiate(lavaPrefab);
                econScript.SpendCoin(lavaScript._cost);
            }
            else if (num < 50)
            {
                Instantiate(bombSPrefab);
                econScript.SpendCoin(bombStrikeScript._cost);
            }

        }
    }
}
