using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    int first = 0;

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
        transform.position = new Vector3(35, -8, 0);
        for (int i = 0; i < 40; i++)
        {
            print("rise");
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        print("Risen");
        yield return new WaitForSeconds(5);
        print("die");
        Destroy(gameObject);

    }
}
