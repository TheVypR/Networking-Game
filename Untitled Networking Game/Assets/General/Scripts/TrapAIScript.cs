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
    public GameObject blind;
    public GameObject glue;


    public GameObject econManager;

    BombStrikeScript bombStrikeScript;
    LavaScript lavaScript;
    BlindScript blindScript;
    EconomyScript econScript;

    private int MAX_TRAP_COST = 100;
    public int openingTrapNum;

    public int _costBombStrike = 50;
    public int _costLava = 75;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < openingTrapNum; i++)
        {
            int r = rando.Next(0, spawners.transform.childCount - 1);
            Transform spawn = spawners.transform.GetChild(r);

            int trap = Random.Range(0, 10);

            if (trap <= 5)
            {
                Instantiate(mine, spawn);
            } else if(trap <= 7)
            {
                Instantiate(glue, spawn);
            } else
            {
                Instantiate(blind, spawn);
            }
            spawn.parent = null;
        }
        /*int r = rando.Next(0, 3);
        Transform spawn = spawners.transform.GetChild(r);
        Instantiate(mine, spawn);
        spawn.parent = null;*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (econManager.GetComponent<EconomyScript>().money > _costLava)
        {
            if (rando.Next(0, 5000) == 1)
            {
                Instantiate(lavaPrefab);
                econManager.GetComponent<EconomyScript>().SpendCoin(_costLava);
            }
        }

        if (econManager.GetComponent<EconomyScript>().money > _costBombStrike)
        {
            if (rando.Next(0, 50000) < 6)
            {
                Instantiate(bombSPrefab);
                econManager.GetComponent<EconomyScript>().SpendCoin(_costBombStrike);
            }
        }

        //if (econManager.GetComponent<EconomyScript>().money > _costBlind)
        //{
        //    if (rando.Next(0, 5000) == 3)
        //    {
        //        Instantiate(blindPrefab);
        //        econManager.GetComponent<EconomyScript>().SpendCoin(_costBlind);
        //    }
        //}     
    }
}
