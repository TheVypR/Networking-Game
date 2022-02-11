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
    public EconomyScript econScript;
    public CameraMotor _cam;

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

        int rand = Random.Range(1, 250);
        if (rand < 25)
        {
            if (econManager.GetComponent<EconomyScript>().money > _costLava)
            {
                Instantiate(lavaPrefab);
                econManager.GetComponent<EconomyScript>().SpendCoin(_costLava);
            }
        }
        else if (rand < 85)
        {
            if (econManager.GetComponent<EconomyScript>().money > _costBombStrike)
            {
                Instantiate(bombSPrefab);
                econManager.GetComponent<EconomyScript>().SpendCoin(_costBombStrike);
            }
        } else
        {
            if(econScript.money > 50)
            {
                econScript.CameraSpeed(Random.Range(0, 2));
            }
        }
    }
}
