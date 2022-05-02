using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : TrapScript
{
    Vector3 camOffset = new Vector3(0, -4, 0);

    float liveTime;

    int charge = 100;
    public override int cost { get { return charge; } set { cost=charge; } }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = gameObject.transform.position - camOffset;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(TriggerLava());
        liveTime = Time.time;
    }

    private void FixedUpdate()
    {
        if(Time.time - liveTime > 4f)
        {
            Destroy(gameObject);
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
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
