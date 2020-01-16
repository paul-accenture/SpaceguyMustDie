using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialText : MonoBehaviour
{
    public int tutorialID;

    //UPDATE ANY TIME YOU ADD A NEW TUTORIAL LEVEL
    static int numTutorials = 3;
    gameState gameState;
    bool complete;
    private string[] tutorials = { "This is a two player game. \nYou'll pass the computer back and forth as you take turns.\nPLAYER ONE can see the KEYS. PLAYER TWO can't.\nPress [SPACE] to continue.",
        "PLAYER ONE: Your turn ends when Space Guy DIES. You can place BUGS in his path. \nBut remember, YOUR REAL GOAL IS TO HELP PLAYER TWO COLLECT KEYS." +
            "\nWhen Space Guy dies, PLAYER TWO will take over and try to avoid your BUGS. Make sure that avoiding your BUGS sends Space Guy to the KEYS!" +
            "\n\n[CLICK] ON THE GROUND to place a BUG underneath this KEY.",
        "PLAYER TWO: Your partner has placed a BUG in your way!\n" +
            "[CLICK] on the ground to place a JUMP PAD that will send Space Guy out of harm's way.\n" +
            "You might even find a secret KEY if your partner was thinking ahead."};
    Text mainText;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameState>();

        mainText = GameObject.FindGameObjectWithTag("MainTextDisplay").GetComponent<Text>();
        StartCoroutine(initializeState());
    }

    // Update is called once per frame
    void Update()
    {

        mainText.text = tutorials[tutorialID];
        if (Input.GetKeyDown(KeyCode.Space))
            switch (tutorialID)
            {
                case (1):
                    {
                       
                        if (complete)
                            StartCoroutine(loadNextLevel(3f));
                        break;
                    }
                case 2:
                    {
                        if(complete)
                            StartCoroutine(loadNextLevel(4f));
                        break;
                    }
                default:
                    {
                        SceneManager.LoadScene("Tutorial 2");

                        break;
                    }
            }
    }

    public void action(indicator source)
    {
        switch (tutorialID)
        {
            case (1):
                {
                    Instantiate(Resources.Load("bug"), source.transform.position, new Quaternion()); 
                    Instantiate(Resources.Load("Poof"), source.transform.position, new Quaternion());

                    Destroy(source.gameObject);
                    tutorials[tutorialID] = "Great job!\n\nNow press [SPACE] to start Space Guy moving and complete your turn.";
                    complete = true;
                    break;
                }
            case 2:
                {
                    Instantiate(Resources.Load("jumpBoard"), source.transform.position + new Vector3(0,-0.15f, 0), new Quaternion());
                    Instantiate(Resources.Load("Poof"), source.transform.position, new Quaternion());

                    Destroy(source.gameObject);
                    tutorials[tutorialID] = "Great job!\n\nNow press [SPACE] to start Space Guy moving and complete your turn.";
                    complete = true;
                    
                    break;
                }
            default:
                {
                    break;
                }

        }
    }

    IEnumerator loadNextLevel(float time)
    {
        
        yield return new WaitForSeconds(time);
        if (tutorialID < numTutorials - 1)
            SceneManager.LoadScene("Tutorial " + (tutorialID + 2));
        else
            SceneManager.LoadScene("Level 1");

    }

    IEnumerator initializeState()
    {
        yield return new WaitForSeconds(0.01f);
        if (tutorialID == 2)
        {
            gameState.updateState(gameState.state.RED);
        }
    }
}
