using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lifetime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position + new Vector3(0,0,10);
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
        StopCoroutine(Lifetime());
    }
}
