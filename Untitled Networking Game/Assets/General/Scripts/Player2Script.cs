using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
    //get traps
    public GameObject[] proxTraps;
    bool player2 = true;
    public GameObject spawners;
    float x;
    Transform[] spawns;
    int place = 0;
    int TRAP_MAX;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        spawns = spawners.GetComponentsInChildren<Transform>();
        TRAP_MAX = spawns.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (player2)
        {
            if (MyInput.GetXAxis(3) >= 0.1)
            {
                if (place >= TRAP_MAX)
                {
                    place = 0;
                }
                else
                {
                    place++;
                }
                target = spawners.transform.Find("SpwnPt " + place);
            }

            if (MyInput.GetXAxis(3) <= -0.1)
            {
                if (place < 0)
                {
                    place = TRAP_MAX - 1;
                }
                else
                {
                    place--;
                }
                target = spawners.transform.Find("SpwnPt " + place);
            }

            //trap placing
            if (MyInput.GetPS4X(1))
            {
                if (proxTraps.Length > 0)
                {
                    Instantiate(proxTraps[0]);
                }
            }
            else if (MyInput.GetPS4Square(1))
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
        print(place);
    }
}
