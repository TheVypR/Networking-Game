using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombStrikeScript : MonoBehaviour
{
    public GameObject _bombPrefab;
    public Transform _bombSpwn;
    
    //control vars
    public float _moveSpeed = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x - 15, Camera.main.transform.position.y + 5, 0);
        StartCoroutine(SpawnBomb());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(_moveSpeed, 0, 0);
        if (transform.position.x > Camera.main.transform.position.x + 15)
        {
            StopCoroutine(SpawnBomb());
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
    }

    IEnumerator SpawnBomb()
    {
        while(true)
        {
            Instantiate(_bombPrefab, _bombSpwn.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
