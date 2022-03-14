using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : TrapScript
{
    int first = 0;

    int charge = 100;
    public override int cost { get { return charge; } set { cost=charge; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (first == 0)
        {
            print(first);
            first = 1;
            StartCoroutine(TriggerLava());
        }
    }

    IEnumerator TriggerLava()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 20);
        for (int i = 0; i < 80; i++)
        {
            transform.position += new Vector3(0, 0.05f, 0);
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
