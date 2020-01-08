using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameState : MonoBehaviour
{

    public enum state { RED, GREEN, CLEAR }
    state myState;

    public Text mainText;

    public GameObject[] HUDkeys;
    public bool[] keysGathered;
    // Start is called before the first frame update
    void Start()
    {
        resetKeys();
        mainText = GameObject.FindGameObjectWithTag("MainTextDisplay").GetComponent<Text>();
        updateState(state.GREEN);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    overlay.color = new Color(1, 1, 1, 0);
                    break;
                }
            case state.RED:
                {
                    mainText.text = "SAVE SPACEGUY!";
                    overlay.color = new Color(.8f, .1f, .1f, .28f);
                    hideKeys();
                    break;
                }
            case state.GREEN:
                {
                    mainText.text = "SPACE GUY MUST DIE!";
                    overlay.color = new Color(.1f, .8f, .1f, .28f);
                    showKeys();
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
        for(int i = 0; i < keys.Length; i++)
        {
            if(!keysGathered[i])
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

       
        HUDkeys = new GameObject[keysGathered.Length];
        

        Debug.Log("Length of Keys array: " + HUDkeys.Length);
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
        Debug.Log("Keys gathered: " + count);
        return count;
    }

    public bool hasAllKeys()
    {
        return (numKeysGathered() == keysGathered.Length);

    }

}
