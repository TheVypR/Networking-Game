using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
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
                //bring up trap display canvas
            } else if (MyInput.GetPS4Square(1))
            {

            }
            else if (MyInput.GetPS4Circle(1))
            {

            }
            else if (MyInput.GetPS4Triangle(1))
            {

            }
        }
    }

    private void FixedUpdate()
    {

        gameObject.transform.position = target.position;

    }
}
