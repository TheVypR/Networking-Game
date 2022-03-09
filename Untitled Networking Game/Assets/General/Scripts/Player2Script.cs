using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
    //get traps
    public GameObject[] proxTraps;
    bool player2 = false;
    public GameObject spawners;
    float x;
    int spawns;
    int place = 0;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        spawns = spawners.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

        if (player2)
        {
            if (MyInput.GetXAxis(1) > 0)
            {
                if (place >= spawns)
                {
                    spawns = 0;
                }
                else
                {
                    place++;
                }
                target = spawners.transform.GetChild(place);
            }

            if (MyInput.GetXAxis(1) < 0)
            {
                if (place < 0)
                {
                    place = spawns - 1;
                }
                else
                {
                    place--;
                }
                target = spawners.transform.GetChild(place);
            }

            //trap placing
            if (MyInput.GetPS4X(1))
            {
                if (proxTraps.Length > 0)
                {
                    Instantiate(proxTraps[0]);
                }
            } else if (MyInput.GetPS4Square(1))
            {
                if (proxTraps.Length > 1)
                {
                    Instantiate(proxTraps[1]);
                }
            }
            else if (MyInput.GetPS4Circle(1))
            {
                if (proxTraps.Length > 2)
                {
                    Instantiate(proxTraps[2]);
                }
            }
            else if (MyInput.GetPS4Triangle(1))
            {
                if (proxTraps.Length > 3)
                {
                    Instantiate(proxTraps[3]);
                }
            }
        }
    }

    private void FixedUpdate()
    {

        gameObject.transform.position = target.position;

    }
}
