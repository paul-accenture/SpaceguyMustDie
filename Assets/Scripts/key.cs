using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    public int keyNumber;
    private gameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            gameState.getKey(keyNumber);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Renderer>().enabled = true;
            GetComponent<Animator>().SetBool("gathered", true);
            
        }
    }
}
