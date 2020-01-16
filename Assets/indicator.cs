using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicator : MonoBehaviour
{
    public tutorialText tutorial;

    SpriteRenderer mySprite;
    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitTriggers = true;
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.enabled = false;
        StartCoroutine(appear());
    }

    // Update is called once per frame
    void Update()
    {
        
        mySprite.color = new Color(1, 1, 1, mySprite.color.a - 0.02f);
        if(mySprite.color.a < 0)
            mySprite.color = new Color(1, 1, 1, 1);



      
    }

    private void OnMouseDown()
    {
        tutorial.action(this);
    }

    IEnumerator appear()
    {
        yield return new WaitForSeconds(3);
        mySprite.enabled = true;
    }
}
