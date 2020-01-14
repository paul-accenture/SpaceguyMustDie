using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck : MonoBehaviour
{

    private player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            StartCoroutine(player.duck());
        }
    }
}
