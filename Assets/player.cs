using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float maxSpeed;
    private Rigidbody2D rigidbody2D;
    Vector2 accel;
    bool alive;

    
    private gameState gameState;
    

    // Start is called before the first frame update
    void Start()
    {
        alive = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        accel = new Vector2(10, 0);
       
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameState>();
        gameState.resetKeys();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (alive && rigidbody2D.velocity.magnitude < maxSpeed)
            rigidbody2D.AddForce(accel);

        if (this.transform.position.y <= -5)
        {
            kill(false);
        }

        if (this.transform.position.y <= -10)
        {
            
            reset();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            kill(true);
        if (collision.gameObject.tag == "boundary")
            reverse();
    }

    private void kill(bool fromEnemy)
    {
        alive = false;
        if (fromEnemy)
            rigidbody2D.AddForce(new Vector2(-100, 100));
        GetComponent<CapsuleCollider2D>().enabled = false;
        gameState.updateState(gameState.state.RED);

    }

    public Rigidbody2D GetRigidbody2D()
    {
        return GetComponent<Rigidbody2D>();
    }

    public void stop()
    {
        alive = false;
        rigidbody2D.velocity = new Vector2(0, 0);
    }

    
    public void reset()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        Vector3 respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
        rigidbody2D.position = (respawn );
        GetComponent<CapsuleCollider2D>().enabled = true;
        gameState.resetKeys();
    }

    public void go() { alive = true; }

    public void reverse()
    {
        accel.x = 0 - accel.x;
        
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }


}
