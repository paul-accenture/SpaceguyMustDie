using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetermineLevelForSwitchScene : MonoBehaviour
{
    public int id = 0;
    string activeScene;
	void Awake()
	{

        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;

        if (activeScene.StartsWith("Level"))
            id = System.Int32.Parse(activeScene.Substring(5));
    }

    private void Update()
    {
        activeScene = SceneManager.GetActiveScene().name;

        if(id > 0 && activeScene.Equals("Tutorial 1"))
        {
            Destroy(this.gameObject);

        }
        if (Input.GetKeyDown(KeyCode.Space) && activeScene.Equals("SwitchScene"))
        {
            id++;
            SceneManager.LoadScene("Level " + (id));
        }
    }
}
