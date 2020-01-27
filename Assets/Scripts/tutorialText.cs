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
    private string[] tutorials = { "Welcome to SPACE GUY MUST DIE!\n" +
            "Two players will work together to gather KEYS.\n" +
            "Pass the computer back and forth as you take turns.\n" +
            "PLAYER ONE can see the KEYS, but "+
            "PLAYER TWO can't.\n" +
            "Press [SPACE] to continue.",

            "PLAYER ONE: Your turn ends when Space Guy DIES from hitting a bug\n" +
            "But remember, YOUR REAL GOAL IS TO HELP PLAYER TWO COLLECT KEYS.\n" +
            "When Space Guy dies, PLAYER TWO will take over and avoid the bugs, get the keys, and get to a flag!\n" +
            "\n[CLICK] ON THE GROUND to place a BUG underneath this KEY.",

            "PLAYER TWO: Your goal is to collect all the hidden KEYS,\navoid death, and get to a FLAG.\n" +
            "Your partner has placed a BUG in the path\nto guide you towards a hidden KEY.\n" +
            "[CLICK] on the ground to place a JUMP PAD that will send Space Guy out of harm's way.\n"
    };
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
        {
            switch (tutorialID)
            {
                case (1):
                    {

                        if (complete)
                        {
                            mainText.text = "";
                            StartCoroutine(loadNextLevel(2.5f));
                        }
                            
                        break;
                    }
                case 2:
                    {
                        if (complete)
                        {
                            tutorials[tutorialID] = "";
                            StartCoroutine(loadNextLevel(3f));
                        }
                            
                        break;
                    }
                default:
                    {
                        SceneManager.LoadScene("Tutorial 2");
                        break;
                    }
            }
        }


        
    }

    public void action(indicator source)
    {
        switch (tutorialID)
        {
            case (1):
                {
                    if (complete == false)
                    {
                        Instantiate(Resources.Load("bug"), source.transform.position, new Quaternion());
                        Instantiate(Resources.Load("Poof"), source.transform.position, new Quaternion());

                        Destroy(source.gameObject);
                        tutorials[tutorialID] = "Great job!\n\nNow press [SPACE] to start Space Guy moving and complete your turn.";
                        complete = true;
                    }

                    break;
                }
            case 2:
                {
                    if (complete == false)
                    {
                        Instantiate(Resources.Load("jumpBoard"), source.transform.position + new Vector3(0, -0.15f, 0), new Quaternion());
                        Instantiate(Resources.Load("Poof"), source.transform.position, new Quaternion());

                        Destroy(source.gameObject);
                        tutorials[tutorialID] = "Great job!\n\nNow press [SPACE] to start Space Guy moving and complete your turn.";
                        complete = true;
                    }
              
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
            SceneManager.LoadScene("SwitchScene");

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
