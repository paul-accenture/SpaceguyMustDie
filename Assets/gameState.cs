using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameState : MonoBehaviour
{

    public enum state { RED, GREEN, CLEAR }
    state myState;
    public bool verboseTutorials;

    public Text mainText;
    private Text powersText;
    private Text playerText;
    private Image powerImage;

    public GameObject[] HUDkeys;
    public bool[] keysGathered;

    public int enemiesLeft;
    public int jumpsLeft;

    public bool inputEnabled;
    public int levelID;
    public int totalLevels;

    private player player;
    // Start is called before the first frame update
    void Start()
    {
        inputEnabled = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        resetKeys();
        mainText = GameObject.FindGameObjectWithTag("MainTextDisplay").GetComponent<Text>();
        powersText = GameObject.FindGameObjectWithTag("PowersText").GetComponent<Text>();
        powerImage = GameObject.FindGameObjectWithTag("PowerImage").GetComponent<Image>();
        playerText = GameObject.FindGameObjectWithTag("playerText").GetComponent<Text>();
        updateState(state.GREEN);

    }

    private void FixedUpdate()
    {
        if(playerText.color.a > 0)
        {
            playerText.color = new Color(1, 1, 1, playerText.color.a - .01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                inputEnabled = false;
                mainText.gameObject.SetActive(false);
                player.go();
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

        if (myState == state.RED)
        {
            powersText.text = "x" + jumpsLeft + " [CLICK]";
        }
        else if (myState == state.GREEN)
        {
            powersText.text = "x" + enemiesLeft + " [CLICK]";
        }
        else
            powersText.text = "";

    }

    public void updateState(state newState)
    {
        bool change = (myState != newState);
        

        myState = newState;
        SpriteRenderer overlay = GameObject.FindGameObjectWithTag("overlay").GetComponent<SpriteRenderer>();
        switch (myState)
        {
            case state.CLEAR:
                {
                    mainText.text = "LEVEL CLEAR!";
                    inputEnabled = false;
                    overlay.color = new Color(1, 1, 1, 0);
                    mainText.gameObject.SetActive(true);
                    StartCoroutine(loadNextLevel());
                    break;
                }
            case state.RED:
                {
                    mainText.text = "SAVE SPACEGUY!\n";
                    if (verboseTutorials)
                    {
                        mainText.text += "\nClick the ground to add a jump pad that will save Space Guy's life -- avoid bugs!";
                        mainText.text += "\nYou can take back jump pads you've already placed by clicking again.";
                        mainText.text += "\n\nPress [SPACE] to send Space Guy onward to freedom!";
                    }
                    overlay.color = new Color(.8f, .1f, .1f, .28f);
                    mainText.color = new Color(1f, 0f, .0f, 1f);
                    powerImage.sprite = Resources.Load<Sprite>("jump");
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 84);
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 94);
                    hideKeys();

                    
                    StartCoroutine(delayInput(2, "PLAYER TWO", change));
                    
                   
                    break;
                }
            case state.GREEN:
                {
                    mainText.text = "SPACE GUY MUST DIE!\n";
                    if (verboseTutorials)
                    {
                        mainText.text += "\nClick the ground to add an obstacle that Space Guy can't avoid without getting the key.";
                        mainText.text += "\nYou can take back enemies you've already placed by clicking again.";
                        mainText.text += "\n\nPress [SPACE] to send Space Guy to his doom!";
                    }
                    overlay.color = new Color(.1f, .8f, .1f, .28f);
                    mainText.color = new Color(0f, 1f, 0f, 1f);
                    powerImage.sprite = Resources.Load<Sprite>("bugSprite");
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 64);
                    showKeys();
                    
                    StartCoroutine(delayInput(1, "PLAYER ONE", change));

                   
                    break;
                }
            default:
                { break; }
        }
        if(SceneManager.GetActiveScene().name == "Level 0")
        {
            mainText.text = "Welcome to SPACE GUY MUST DIE!\n";
            mainText.text += "\nThis is a two player game -- you will work together.\nPLAYER ONE ";
            mainText.text += "will be able to see the keys Space Guy";
            mainText.text += "\nneeds to collect, but can't affect how he moves.";
            mainText.text += "\nPLAYER TWO can't see the keys but can help";
            mainText.text += "\nSpace Guy jump. Player One can place obstacles";
            mainText.text += "\nto force Space Guy along the right path under";
            mainText.text += "\nthreat of death. Take turns -- Player One when";
            mainText.text += "\nthe screen is GREEN and Player Two when the ";
            mainText.text += "\nscreen is RED (no peeking!)";
            mainText.text += "\nPress [SPACE] to start!)";

        }
    }

    private void hideKeys()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        for (int i = 0; i < keys.Length; i++)
        {
            //if (!keysGathered[i])
            keys[i].GetComponent<Renderer>().enabled = false;
            keys[i].GetComponent<Collider2D>().enabled = true;
            Debug.Log("hiding " + keys[i].gameObject.name);
        }
    }

    private void showKeys()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        for (int i = 0; i < keys.Length; i++)
        {
            //if (!keysGathered[i])
                keys[i].GetComponent<Renderer>().enabled = true;
                keys[i].GetComponent<Collider2D>().enabled = false;
                Debug.Log("showing " + keys[i].gameObject.name);
        }
    }

    public void getKey(int index)
    {
        keysGathered[index] = true;
        HUDkeys[index].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("filledKey");
        Debug.Log("Player gathered key " + index);

    }

    public void resetKeys()
    {
        keysGathered = new bool[GameObject.FindGameObjectsWithTag("Key").Length];

        if (HUDkeys == null || HUDkeys.Length == 0)
            HUDkeys = new GameObject[keysGathered.Length];



        for (int i = 0; i < HUDkeys.Length; i++)
        {
            if (HUDkeys[i] != null)
                Destroy(HUDkeys[i]);
            HUDkeys[i] = (UnityEngine.GameObject)Instantiate(Resources.Load("HUDkeyslot"), new Vector3((float)(-9.4 + (i * 0.5)), 5, 0), new Quaternion());
        }
    }

    public int numKeysGathered()
    {
        int count = 0;
        for (int i = 0; i < keysGathered.Length; i++)
        {
            if (keysGathered[i])
                count++;
        }

        return count;
    }

    public bool hasAllKeys()
    {
        return (numKeysGathered() == keysGathered.Length);

    }

    public void stopPlayer()
    {
        player.stop();
    }

    public state getState()
    {
        return myState;
    }

    public void resetPlayer()
    {
        player.reset();
    }

    public void spendEnemy()
    {
        enemiesLeft--;
    }

    public void spendJump()
    {
        jumpsLeft--;
    }

    public void gainJump()
    {
        jumpsLeft++;
    }

    public void gainEnemy()
    {
        enemiesLeft++;
    }

    IEnumerator delayInput(int seconds, string text, bool change)
    {
        inputEnabled = false;
        yield return new WaitForSeconds(seconds);
        if (change)
        {
            playerText.text = text;
            playerText.color = new Color(1, 1, 1, 1);
        }
       
        inputEnabled = true;
        mainText.gameObject.SetActive(true);
    }

    IEnumerator loadNextLevel()
    {
        inputEnabled = false;
        yield return new WaitForSeconds(2);
        inputEnabled = true;
        if(levelID < totalLevels)
            SceneManager.LoadScene("Level " + (levelID + 1));
    }

}
