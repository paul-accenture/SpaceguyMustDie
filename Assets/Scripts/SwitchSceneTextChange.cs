using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSceneTextChange : MonoBehaviour
{
    Text switchSceneText;
    // Start is called before the first frame update
    void Start()
    {
        DetermineLevelForSwitchScene idManager = GameObject.FindGameObjectWithTag("sceneIDManager").GetComponent<DetermineLevelForSwitchScene>();
        switchSceneText = GameObject.FindGameObjectWithTag("SwitchSceneText").GetComponent<Text>();

        if (idManager.id < 1)
            switchSceneText.text = "CONGRATULATIONS! You have completed the tutorial! PLAYER ONE, GET READY! (No peeking, Player Two!)\n\nPRESS [SPACE] TO CONTINUE.";
        else
            switchSceneText.text = "NEXT LEVEL LOADED!\nPLAYER ONE, GET READY!\n\nPRESS [SPACE] TO CONTINUE.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
