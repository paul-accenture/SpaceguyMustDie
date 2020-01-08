using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private player player;
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
            gameState.stopPlayer();

            if (gameState.hasAllKeys())
                gameState.updateState(gameState.state.CLEAR);
            else
            { 
                gameState.updateState(gameState.state.GREEN);
                gameState.resetPlayer();
            }
        }
    }

   
}
