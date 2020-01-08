using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    GridLayout gridLayout;
    public GameObject hiliteSprite;
    public Vector3 offset;
    public Tilemap tilemap;


    // Start is called before the first frame update
    void Start()
    {
        gridLayout = GetComponentInParent<GridLayout>();
        tilemap = GetComponentInChildren<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = gridLayout.WorldToCell(mouseWorldPos);
            
            if(getTileAtPosition(coordinate) != null)
                hiliteSprite.transform.SetPositionAndRotation(gridLayout.CellToWorld(coordinate) + offset, new Quaternion()) ;

        }

        if (Input.GetKeyUp(KeyCode.J) && getActiveTile() != null)
        {
            Instantiate(Resources.Load("jumpBoard"), hiliteSprite.transform.position + new Vector3( 0, (float).7, 0), hiliteSprite.transform.rotation);
        }

        if (Input.GetKeyUp(KeyCode.E) && getActiveTile() != null)
        {
            Debug.Log("enemy");
            Instantiate(Resources.Load("pokerMad"), hiliteSprite.transform.position + new Vector3(0, (float).4, 0), hiliteSprite.transform.rotation);
        }

    }

    TileBase getActiveTile()
    {

        Vector3Int coordinate = gridLayout.WorldToCell(hiliteSprite.transform.position - offset);

        return tilemap.GetTile(coordinate);
        
    }

    TileBase getTileAtPosition(Vector3Int pos)
    {
        
        TileBase tile = tilemap.GetTile(pos);
       
        return tile;

    }

}
