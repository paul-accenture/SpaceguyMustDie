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
    private Image powerImage;

    public GameObject[] HUDkeys;
    public bool[] keysGathered;

    public int enemiesLeft;
    public int jumpsLeft;

    public bool inputEnabled;

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
        updateState(state.GREEN);

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
            powersText.text = "x" + jumpsLeft + " [J]";
        }
        else if (myState == state.GREEN)
        {
            powersText.text = "x" + enemiesLeft + " [E]";
        }
        else
            powersText.text = "";

    }

    public void updateState(state newState)
    {
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
                    break;
                }
            case state.RED:
                {
                    mainText.text = "SAVE SPACEGUY!\n";
                    if (verboseTutorials)
                    {
                        mainText.text += "\nSelect a tile and press [J] to add a jump pad that will save Space Guy's life.";
                        mainText.text += "\nYou can take back jump pads you've already placed by pressing [J] with their tile selected.";
                        mainText.text += "\n\nPress [SPACE] to send Space Guy onward to freedom!";
                    }
                    overlay.color = new Color(.8f, .1f, .1f, .28f);
                    powerImage.sprite = Resources.Load<Sprite>("jump");
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 84);
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 94);
                    hideKeys();

                    
                    StartCoroutine(delayInput(2));
                    

                    break;
                }
            case state.GREEN:
                {
                    mainText.text = "SPACE GUY MUST DIE!\n";
                    if (verboseTutorials)
                    {
                        mainText.text += "\nSelect a tile and press [E] to add an enemy that will force Space Guy to collect the key.";
                        mainText.text += "\nYou can take back enemies you've already placed by pressing [E] with their tile selected.";
                        mainText.text += "\n\nPress [SPACE] to send Space Guy to his doom!";
                    }
                    overlay.color = new Color(.1f, .8f, .1f, .28f);
                    powerImage.sprite = Resources.Load<Sprite>("poker");
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 48);
                    powerImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 128);
                    showKeys();
                    
                    StartCoroutine(delayInput(1));
                    
                    break;
                }
            default:
                { break; }
        }

    }

    private void hideKeys()
    {
        foreach (GameObject k in GameObject.FindGameObjectsWithTag("Key"))
        {
            k.GetComponent<Renderer>().enabled = false;
        }
    }

    private void showKeys()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        for (int i = 0; i < keys.Length; i++)
        {
            if (!keysGathered[i])
                keys[i].GetComponent<Renderer>().enabled = true;
        }
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



        for (int i = 0; i < keysGathered.Length; i++)
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

    IEnumerator delayInput(int seconds)
    {
        inputEnabled = false;
        yield return new WaitForSeconds(seconds);
        inputEnabled = true;
        mainText.gameObject.SetActive(true);
    }

}
