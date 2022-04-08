using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player2Script : PlayerBaseScript
{
    //get traps
    public GameObject[] proxTraps;
    public GameObject[] manualTraps;

    //guide canvases
    public Canvas setupCanvas;
    public Canvas gameCanvas;
    Text[] proxCosts;
    Text[] manCosts;
    public GameObject indicator;

    public GameObject spawners;
    public EconomyScript _econ;
    Transform[] spawns;
    int place = 0;
    float lastSwitch;
    public float SWITCH_RATE = 0.25f;
    int TRAP_MAX;
    Transform target;

    //check mode
    bool isSetup = true;

    // Start is called before the first frame update
    void Start()
    {
        spawns = spawners.GetComponentsInChildren<Transform>();
        TRAP_MAX = spawns.Length;
        transform.position = spawners.transform.Find("SpwnPt " + place).position;

        //set trap costs
        proxCosts = setupCanvas.GetComponentsInChildren<Text>();
        manCosts = gameCanvas.GetComponentsInChildren<Text>();

        //set the costs for proximity traps
        for(int i = 0; i < proxTraps.Length; i++) {
            if(i < proxCosts.Length)
            {
                proxCosts[i].text = proxTraps[i].GetComponent<TrapScript>().cost.ToString();
            }else
            {
                break;
            }
        }

        //set the costs for manual traps
        for (int i = 0; i < manualTraps.Length; i++)
        {
            if (i < manCosts.Length)
            {
                manCosts[i].text = manualTraps[i].GetComponent<TrapScript>().cost.ToString();
            }
            else
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //navigate the trap locations
        if (hasAuthority)
        {
            if (isSetup)
            {
                if ((Time.time - lastSwitch) >= SWITCH_RATE)
                {
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
                if (MyInput.GetPS4X(1))
                {
                    if (proxTraps.Length > 0)
                    {
                        if (_econ.SpendCoin(proxTraps[0].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(proxTraps[0], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
                else if (MyInput.GetPS4Square(1))
                {
                    if (proxTraps.Length > 1)
                    {
                        if (_econ.SpendCoin(proxTraps[1].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(proxTraps[1], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
                else if (MyInput.GetPS4Circle(1))
                {
                    if (proxTraps.Length > 2)
                    {
                        if (_econ.SpendCoin(proxTraps[2].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(proxTraps[2], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
                else if (MyInput.GetPS4Triangle(1))
                {
                    if (proxTraps.Length > 3)
                    {
                        if (_econ.SpendCoin(proxTraps[3].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(proxTraps[3], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
            }
            else
            {
                //control camera speed
                if ((Time.time - lastSwitch) >= SWITCH_RATE)
                {
                    if (MyInput.GetXAxis(3) > 0.05)
                    {
                        lastSwitch = Time.time;
                        _econ.CameraSpeed(1);
                    }

                    if (MyInput.GetXAxis(3) < -0.05)
                    {
                        _econ.CameraSpeed(0);
                        lastSwitch = Time.time;
                    }
                }

                //trigger manual traps
                if (MyInput.GetPS4Circle(1))
                {
                    if (manualTraps.Length > 0)
                    {
                        if (_econ.SpendCoin(manualTraps[0].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(manualTraps[0], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
                else if (MyInput.GetPS4X(1))
                {
                    if (manualTraps.Length > 1)
                    {
                        if (_econ.SpendCoin(manualTraps[1].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(manualTraps[1], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
                else if (MyInput.GetPS4Square(1))
                {
                    if (manualTraps.Length > 2)
                    {
                        if (_econ.SpendCoin(manualTraps[2].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(manualTraps[2], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
                else if (MyInput.GetPS4Triangle(1))
                {
                    if (manualTraps.Length > 3)
                    {
                        if (_econ.SpendCoin(manualTraps[3].GetComponent<TrapScript>().cost))
                        {
                            GameObject trap = Instantiate(manualTraps[3], transform.position, Quaternion.identity);
                            CmdSpawnTrap(trap);
                        }
                    }
                }
            }
        }
    }

    [Command]
    private void CmdSpawnTrap(GameObject trap)
    {
        NetworkServer.Spawn(trap);
    }

    private void FixedUpdate()
    {
        if (target)
        {
            if (isSetup)
            {
                gameObject.transform.position = target.position;
            } else
            {
                gameObject.transform.position = Camera.main.transform.position;
            }
        }
    }

    public void setMode(bool isSetup)
    {
        this.isSetup = isSetup;
        if (isSetup)
        {
            setupCanvas.enabled = true;
            gameCanvas.enabled = false;
            indicator.SetActive(true);
        }
        else
        {
            setupCanvas.enabled = false;
            gameCanvas.enabled = true;
            indicator.SetActive(false);
        }
    }
}
