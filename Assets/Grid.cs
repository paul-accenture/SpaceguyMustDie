using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour
{
    GridLayout gridLayout;
    public GameObject hiliteSprite;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        //gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        gridLayout = GetComponentInParent<GridLayout>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = gridLayout.WorldToCell(mouseWorldPos);
            hiliteSprite.transform.SetPositionAndRotation(gridLayout.CellToWorld(coordinate) + offset, new Quaternion()) ;

        }

        if(Input.GetKeyUp(KeyCode.J))
        {
            Instantiate(Resources.Load("jumpBoard"), hiliteSprite.transform.position + new Vector3( 0, (float).7, 0), hiliteSprite.transform.rotation);
        }
    }
}
