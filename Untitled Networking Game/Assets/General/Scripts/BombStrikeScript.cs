using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class BombStrikeScript : TrapScript
{
    public GameObject _bombPrefab;
    public Transform _bombSpwn;
    
    //control vars
    public float _moveSpeed = 0.1f;

    int charge = 100;
    public override int cost { get { return charge; } set { cost = charge; } }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x - 15, Camera.main.transform.position.y + 5, 0);
        StartCoroutine(SpawnBomb());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(_moveSpeed, 0, 0);
        if (transform.position.x > Camera.main.transform.position.x + 15)
        {
            StopCoroutine(SpawnBomb());
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnBomb()
    {
        while(true)
        {
            //only do network spawn if online game
            if (PlayerPrefs.HasKey("mode"))
            {
                if (PlayerPrefs.GetInt("mode") == 2)
                {
                    if (isServer)
                    {
                        GameObject trap = Instantiate(_bombPrefab, _bombSpwn.position, Quaternion.identity);
                        NetworkServer.Spawn(trap);
                    }
                } else
                {
                    Instantiate(_bombPrefab, _bombSpwn.position, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
