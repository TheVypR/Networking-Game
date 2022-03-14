using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
    //get traps
    public GameObject[] proxTraps;
    public GameObject spawners;
    Transform[] spawns;
    int place = 0;
    float lastSwitch;
    public float SWITCH_RATE = 0.25f;
    int TRAP_MAX;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        spawns = spawners.GetComponentsInChildren<Transform>();
        TRAP_MAX = spawns.Length;
        transform.position = spawners.transform.Find("SpwnPt " + place).position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastSwitch) >= SWITCH_RATE) {
            if (MyInput.GetRawXAxis(3) == 1)
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
                lastSwitch = Time.time;
            }

            if (MyInput.GetRawXAxis(3) == -1)
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
                lastSwitch = Time.time;
            }
        }
        //trap placing
        if (Input.GetKeyDown(KeyCode.L))
            {
                if (proxTraps.Length > 0)
                {
                    Instantiate(proxTraps[0], transform.position, Quaternion.identity);
                }
            }
            else if (MyInput.GetPS4Square(1))
            {
                if (proxTraps.Length > 1)
                {
                    Instantiate(proxTraps[1], transform.position, Quaternion.identity);
                }
            }
            else if (MyInput.GetPS4Circle(1))
            {
                if (proxTraps.Length > 2)
                {
                    Instantiate(proxTraps[2], transform.position, Quaternion.identity);
                }
            }
            else if (MyInput.GetPS4Triangle(1))
            {
                if (proxTraps.Length > 3)
                {
                    Instantiate(proxTraps[3], transform.position, Quaternion.identity);
                }
            }
        
    }

    private void FixedUpdate()
    {
        
        gameObject.transform.position = target.position;
        print(place);
    }
}
