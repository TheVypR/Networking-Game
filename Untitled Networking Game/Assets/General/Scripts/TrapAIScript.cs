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
    public EconomyScript econScript;
    public CameraMotor _cam;

    public int openingTrapNum;

    private float camSlowTime = 0f;

    public int _costBombStrike = 50;
    public int _costLava = 50;
    public int _costCamSlow = 50;
    private int spendAt;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToStart());
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(1f);

        //start trapping
        for (int i = 0; i < openingTrapNum; i++)
        {
            int r = rando.Next(0, spawners.transform.childCount - 1);
            Transform spawn = spawners.transform.GetChild(r);

            int trap = Random.Range(0, 10);

            if (trap <= 5)
            {
                Instantiate(mine, spawn);
            }
            else if (trap <= 7)
            {
                Instantiate(glue, spawn);
            }
            else
            {
                Instantiate(blind, spawn);
            }
            spawn.parent = null;
        }

        spendAt = Random.Range(40, 120);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.HasKey("mode"))
        {
            if (PlayerPrefs.GetInt("mode") == 0)
            {
                if (econScript.money > spendAt)
                {
                    int rand = Random.Range(1, 250);
                    if (rand < 25)
                    {
                        if (econManager.GetComponent<EconomyScript>().money > _costLava)
                        {
                            Instantiate(lavaPrefab, new Vector2(_cam.transform.position.x, _cam.transform.position.y - 20), Quaternion.identity);
                            econManager.GetComponent<EconomyScript>().SpendCoin(_costLava);
                            spendAt = Random.Range(40, 120);
                        }
                    }
                    else if (rand < 85)
                    {
                        if (econManager.GetComponent<EconomyScript>().money > _costBombStrike)
                        {
                            Instantiate(bombSPrefab);
                            econManager.GetComponent<EconomyScript>().SpendCoin(_costBombStrike);
                            spendAt = Random.Range(40, 120);
                        }
                    }
                    else
                    {
                        if (econScript.money > 75)
                        {
                            econScript.CameraSpeed(Random.Range(0, 2) == 1);
                            camSlowTime = Random.Range(10, 30);
                            StartCoroutine(camSlowEnum());
                            spendAt = Random.Range(40, 120);
                        }
                    }
                }
            }
        }
    }

    IEnumerator camSlowEnum()
    {
        yield return new WaitForSeconds(camSlowTime);
        econScript.stopCamSpeed();
        camSlowTime = 0;
    }
}
