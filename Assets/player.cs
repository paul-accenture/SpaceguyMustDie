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
    
    public Text mainText;
   
    // Start is called before the first frame update
    void Start()
    {
        alive = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        accel = new Vector2(10, 0);

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            alive = true;
        }
       
        if(alive && rigidbody2D.velocity.magnitude < maxSpeed)
            rigidbody2D.AddForce(accel);

        if (this.transform.position.y <= -5)
        {
            kill(false);
        }

        if (this.transform.position.y <= -10)
        {
            SceneManager.LoadScene("SampleScene");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            kill(true);
    }

    private void kill(bool fromEnemy)
    {
        alive = false;
        if(fromEnemy)
            rigidbody2D.AddForce(new Vector2(-100, 100));
        GetComponent<CapsuleCollider2D>().enabled = false;
        mainText.text = "O NOES U DED";

    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rigidbody2D;
    }

    public void Deactivate()
    {
        alive = false;
        rigidbody2D.velocity = new Vector2(0, 0);
    }
}
