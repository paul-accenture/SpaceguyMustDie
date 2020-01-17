using System.Collections;
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
    private Image altPowerImage;

    public GameObject[] HUDkeys;
    public bool[] keysGathered;

    public int enemiesLeft;
    public int altEnemiesLeft;
    public int jumpsLeft;
    public int ducksLeft;
    bool noAltPowers;

    public bool inputEnabled;
    public int levelID;
    public int totalLevels;
    SpriteRenderer overlay;
    float idleTime;
    private DetermineLevelForSwitchScene idManager;

    private player player;
    // Start is called before the first frame update
    void Start()
    {
        inputEnabled = true;
        idManager = GameObject.FindGameObjectWithTag("sceneIDManager").GetComponent<DetermineLevelForSwitchScene>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        resetKeys();
        mainText = GameObject.FindGameObjectWithTag("MainTextDisplay").GetComponent<Text>();
        powersText = GameObject.FindGameObjectWithTag("PowersText").GetComponent<Text>();
        powerImage = GameObject.FindGameObjectWithTag("PowerImage").GetComponent<Image>();
        altPowerImage = GameObject.FindGameObjectWithTag("altPowerImage").GetComponent<Image>();
        playerText = GameObject.FindGameObjectWithTag("playerText").GetComponent<Text>();
        overlay = GameObject.FindGameObjectWithTag("overlay").GetComponent<SpriteRenderer>();

        noAltPowers = (altEnemiesLeft == 0 && ducksLeft == 0);
        
        if(levelID == 0)
            updateState(state.RED);
        else
            updateState(state.GREEN);

        if (noAltPowers)
            altPowerImage.color = new Color(1, 1, 1, 0);

    }

    private void FixedUpdate()
    {
        if(playerText.color.a > 0)
        {
            playerText.color = new Color(1, 1, 1, playerText.color.a - .01f);
        }
        if(overlay.color.a > 0.28f)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, overlay.color.a - .005f);
        }
        idleTime += Time.fixedDeltaTime;

        if (idleTime > 120 && !SceneManager.GetActiveScene().name.Equals("Tutorial 1"))
            SceneManager.LoadScene("Tutorial 1");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                idleTime = 0;
                if (levelID == totalLevels)
                {
                    SceneManager.LoadScene("Tutorial 1");
                }
                else
                {
                    inputEnabled = false;
                    mainText.gameObject.SetActive(false);
                    player.go();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

        if (myState == state.RED)
        {
            powersText.text = "x" + jumpsLeft + " [LMB]";
            if (!noAltPowers)
            {
                powersText.text += "\n\nx" + ducksLeft + " [RMB]";
            }
        }
        else if (myState == state.GREEN)
        {
            powersText.text = "x" + enemiesLeft + " [LMB]";
            if (!noAltPowers)
            {
                powersText.text += "\n\nx" + altEnemiesLeft + " [RMB]";
            }
        }
        else
            powersText.text = "";

    }

    public void updateState(state newState)
    {
        bool change = (myState != newState);
        

        myState = newState;
        
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
                    if (verboseTutorials)
                    {
                        if (noAltPowers)
                        {
                            mainText.text = "\nThe other player has placed BUGS in the map to help guide you to KEY.";
                            mainText.text += "\n[CLICK] the ground to add a JUMP PAD.";
                            mainText.text += "\nYou can take back JUMPS you've already placed by clicking again.";
                            mainText.text += "\n\nPress [SPACE] to send Space Guy onward to freedom!";
                        }
                        else
                        {
                            mainText.text = "[RIGHT CLICK] the ground to add a limbo bar -- Space Guy will duck under obstacles.";
                            mainText.text += "\nThis will also slow him down.";
                            mainText.text += "\nYou can take back items you've already placed by clicking again.";
                            
                        }
                    }
                    overlay.color = new Color(.8f, .1f, .1f, 1);
                    mainText.color = new Color(1f, 0f, .0f, 1f);
                    powerImage.sprite = Resources.Load<Sprite>("jump");
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 84);
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 94);
                    altPowerImage.sprite = Resources.Load<Sprite>("duck");
                    if(!noAltPowers)
                        altPowerImage.color = new Color(1, 1, 1, 1);
                    altPowerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 84);
                    altPowerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 94);
                    hideKeys();

                    
                    StartCoroutine(delayInput(2, "PLAYER TWO", change));
                    
                   
                    break;
                }
            case state.GREEN:
                {
                    if (verboseTutorials)
                    {
                        if (noAltPowers)
                        {
                            mainText.text = "";
                            mainText.text += "[CLICK] the ground to add a BUG to help guide the other player to the KEY";
                            mainText.text += "\nYou can take back BUGS you've already placed by clicking again.";
                            mainText.text += "\n\nPress [SPACE] to send Space Guy to his doom!";
                        }
                        else
                        {
                            mainText.text = "\n[RIGHT CLICK] the ground to place a tall stack of bugs that Space Guy can't jump over.";
                            mainText.text += "\nHe'll have to slow down and duck to get past.";
                            mainText.text += "\nYou can take back enemies you've already placed by clicking again.";
                            
                        }
                    }
                    overlay.color = new Color(.1f, .8f, .1f, 1);
                    mainText.color = new Color(0f, 1f, 0f, 1f);
                    powerImage.sprite = Resources.Load<Sprite>("bugSprite");
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 64);
                    altPowerImage.sprite = Resources.Load<Sprite>("altBugSprite");
                    altPowerImage.color = new Color(1, 0.525f, 0.525f, 1);
                    altPowerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 80);
                    altPowerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150);
                    StartCoroutine(showKeys());
                    
                    StartCoroutine(delayInput(1, "PLAYER ONE", change));

                   
                    break;
                }
            default:
                { break; }
        }
        if (SceneManager.GetActiveScene().name == "Level 0")
        {
            if (myState != state.CLEAR)
            {
                mainText.text = "This is a two player game -- hand the computer";
                mainText.text += "\nback and forth as you play";
                mainText.text += "\n";
                mainText.text += "\nPLAYER ONE must guide Space Guy to the keys and the flag";
                mainText.text += "\nby placing bugs in the level.";
                mainText.text += "\n";
                mainText.text += "\nPLAYER TWO must use the placement of bugs ";
                mainText.text += "\nto collect all the keys and get to the flag";
                mainText.text += "\n";
                mainText.text += "\n\nPress [SPACE] to start!";
            }
            else
                mainText.text = "LET'S GO!";
            
        }
        else if (levelID == totalLevels)
        {
            if (myState != state.CLEAR)
            {
                mainText.text = "Thanks for playing SPACE GUY MUST DIE!\n";
                mainText.text += "\nDid you notice that Player Two had an easier time?\n";
                mainText.text += "\nIn Test Driven Development, writing the right test";
                mainText.text += "\nmakes implementing your next key feature easier.";
                mainText.text += "\nAs you build on the features you've implemented,";
                mainText.text += "\nthe tests you've already written ensure your new";
                mainText.text += "\nfeatures don't break anything.";
                mainText.text += "\n\nBYE-BYE, SPACE GUY!";
                mainText.text += "\nPress [SPACE] to start over.";
                
            }
            else
                mainText.text = "LET'S GO!";

        }
    }

    private void hideKeys()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].GetComponent<Renderer>().enabled = false;
            keys[i].GetComponent<Collider2D>().enabled = true;
            
        }
    }

    private IEnumerator showKeys()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");

        for (int i = 0; i < keys.Length; i++)
            keys[i].GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(4);
        for (int i = 0; i < keys.Length; i++)
            keys[i].GetComponent<Renderer>().enabled = true;
        
    }

    public void getKey(int index)
    {
        keysGathered[index] = true;
        HUDkeys[index].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("filledKey");
       

    }

    public void resetKeys()
    {
        keysGathered = new bool[GameObject.FindGameObjectsWithTag("Key").Length];

        if (HUDkeys == null || HUDkeys.Length == 0)
            HUDkeys = new GameObject[keysGathered.Length];

        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");

        for (int i = 0; i < HUDkeys.Length; i++)
        {
            if (HUDkeys[i] != null)
                Destroy(HUDkeys[i]);
            HUDkeys[i] = (UnityEngine.GameObject)Instantiate(Resources.Load("HUDkeyslot"), new Vector3((float)(-9.4 + (i)), 5, 0), new Quaternion());
            keys[i].GetComponent<Animator>().SetBool("gathered", false);

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

    public void spendEnemy(bool alt)
    {
        if(alt)
            altEnemiesLeft--;
        else
            enemiesLeft--;
    }

    public void spendItem(bool alt)
    {
        if (alt)
            ducksLeft--;
        else
            jumpsLeft--;
    }

    public void gainItem(bool alt)
    {
        if (alt)
            ducksLeft++;
        else
            jumpsLeft++;
    }

    public void gainEnemy(bool alt)
    {
        if (alt)
            altEnemiesLeft++;
        else
            enemiesLeft++;
    }

    IEnumerator delayInput(int seconds, string text, bool change)
    {
        inputEnabled = false;
        //yield return new WaitForSeconds(1);
        if (change)
        {
            playerText.text = text;
            playerText.color = new Color(1, 1, 1, 1);
        }
        yield return new WaitForSeconds(seconds);

        inputEnabled = true;
        mainText.gameObject.SetActive(true);
    }

    IEnumerator loadNextLevel()
    {
        inputEnabled = false;
        yield return new WaitForSeconds(2);
        inputEnabled = true;
        if (levelID < totalLevels - 1)
            SceneManager.LoadScene("SwitchScene");
        else
            SceneManager.LoadScene("Level " + totalLevels);
    }

    public void setIdleTime(float time)
    {
        idleTime = time;
    }

}
