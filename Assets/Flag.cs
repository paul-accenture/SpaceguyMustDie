using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject player = other.gameObject;
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 200));
            Debug.Log("GREEN");
        }
    }
}
