using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluerScript : MonoBehaviour
{
    public GameObject _gluePrefab;
    public Transform _glueSpwn;
    Coroutine _spray;
    
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
            _spray = StartCoroutine(SprayGlue());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            StopCoroutine(_spray);
        }
    }

    IEnumerator SprayGlue()
    {
        while(true)
        {
            Instantiate(_gluePrefab, _glueSpwn.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
