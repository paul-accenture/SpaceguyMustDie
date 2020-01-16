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
            switchSceneText.text = "THE TUTORIAL IS OVER DETERMINE WHO WILL GO FIRST AND PASS THE COMPUTER TO THEM.\n\nPRESS [SPACE] TO CONTINUE.";
        else
            switchSceneText.text = "PASS THE COMPUTER TO THE OTHER PLAYER.\n\nPRESS [SPACE] TO CONTINUE.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
