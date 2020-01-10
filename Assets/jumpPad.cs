using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    private player player;
    public Vector2 jump;
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
            
            Rigidbody2D playerRigidBody = player.GetRigidbody2D();
            float playerVelocityX = playerRigidBody.velocity.x;
            playerRigidBody.velocity = new Vector2(playerVelocityX, 0);
            GetComponent<AudioSource>().Play();
            playerRigidBody.AddForce(jump);
            GetComponent<Animator>().SetBool("jumping", true);
            endAnim();
        }
    }

    IEnumerator endAnim()
    {
        
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetBool("jumping", false);
    }
}
