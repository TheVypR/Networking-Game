using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombStrikeScript : MonoBehaviour
{
    public GameObject _bombPrefab;
    public Transform _bombSpwn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBomb()
    {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(_bombPrefab, _bombSpwn);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
