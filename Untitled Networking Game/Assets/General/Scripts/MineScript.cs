using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    LvlMngrScript _mngr;
    // Start is called before the first frame update
    void Start()
    {
        _mngr = FindObjectOfType<LvlMngrScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            _mngr.PlayerDeath();
    }
}
