using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinderScript : TrapScript
{
    public GameObject _blindPrefab;

    int charge = 75;
    public override int cost { get { return charge; } set { cost = charge; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Instantiate(_blindPrefab);
        }
    }
}
