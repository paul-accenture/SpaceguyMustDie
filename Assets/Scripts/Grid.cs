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

    private gameState gamestate;
    // Start is called before the first frame update
    void Start()
    {
        gamestate = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameState>();
        gridLayout = GetComponentInParent<GridLayout>();
        tilemap = GetComponentInChildren<Tilemap>();
        offset = new Vector2(0.35f, 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate.inputEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
               
                handleClick(Camera.main.ScreenToWorldPoint(Input.mousePosition), true);
                
            }

            if(Input.GetMouseButtonDown(1))
            {
                handleClick(Camera.main.ScreenToWorldPoint(Input.mousePosition), false);
            }
        }

    }

    TileBase getActiveTile()
    {

        Vector3Int coordinate = gridLayout.WorldToCell(hiliteSprite.transform.position - offset);

        return tilemap.GetTile(coordinate);
        
    }

    Tile getTileAtPosition(Vector3Int pos)
    {
        
        Tile tile = (UnityEngine.Tilemaps.Tile)tilemap.GetTile(pos);
       
        return tile;

    }

    TileBase checkForTilesAboveActiveTile()
    {
        Vector3Int coordinate = gridLayout.WorldToCell(hiliteSprite.transform.position - offset);

        coordinate.y += 1;

        return tilemap.GetTile(coordinate);

    }

    GameObject getObjectAtActiveTile()
    {
        Vector2 coordinate = new Vector2((hiliteSprite.transform.position).x, (hiliteSprite.transform.position).y+1);
        Collider2D[] hits = Physics2D.OverlapCircleAll(coordinate, 0.35f);
        foreach (Collider2D i in hits)
        {
            if (i.gameObject.name != "Tilemap")
            {
               
                return i.gameObject;
            }
        }
        return null;
    }

    public void handleClick(Vector3 mousePos, bool left)
    {

        Vector3Int coordinate = gridLayout.WorldToCell(mousePos);

        
        hiliteSprite.transform.SetPositionAndRotation(gridLayout.CellToWorld(coordinate) + offset, new Quaternion());


        if (left)
        {
            if (getActiveTile() != null && checkForTilesAboveActiveTile() == null)
            {
                GameObject alreadyHere = getObjectAtActiveTile();
                if (gamestate.getState() == gameState.state.RED)
                {

                    if (alreadyHere == null)
                    {
                        if (gamestate.jumpsLeft > 0)
                        {
                            Instantiate(Resources.Load("jumpBoard"), hiliteSprite.transform.position + new Vector3(0, (float).7, 0), hiliteSprite.transform.rotation);
                            gamestate.spendItem(false);
                        }
                    }
                    else if (alreadyHere.name.StartsWith("jumpBoard"))
                    {
                        Debug.Log(alreadyHere.name);
                        Destroy(alreadyHere);
                        gamestate.gainItem(false);
                    }
                    else
                        Debug.Log(alreadyHere.name);

                }

                if (gamestate.getState() == gameState.state.GREEN)
                {

                    if (alreadyHere == null)
                    {
                        if (gamestate.enemiesLeft > 0)
                        {
                            Instantiate(Resources.Load("bug"), hiliteSprite.transform.position + new Vector3(0, (float).8, 0), hiliteSprite.transform.rotation);
                            gamestate.spendEnemy(false);
                        }
                    }
                    else if (alreadyHere.name.StartsWith("bug"))
                    {
                        Destroy(alreadyHere);
                        gamestate.gainEnemy(false);
                    }
                }
            }
        }
        else
        {
            if (getActiveTile() != null && checkForTilesAboveActiveTile() == null)
            {
                GameObject alreadyHere = getObjectAtActiveTile();
                if (gamestate.getState() == gameState.state.RED)
                {

                    if (alreadyHere == null)
                    {
                        if (gamestate.ducksLeft > 0)
                        {
                            Instantiate(Resources.Load("duck"), hiliteSprite.transform.position + new Vector3(0, (float).7, 0), hiliteSprite.transform.rotation);
                            gamestate.spendItem(true);
                        }
                    }
                    else if (alreadyHere.name.StartsWith("duck"))
                    {
                        Debug.Log(alreadyHere.name);
                        Destroy(alreadyHere);
                        gamestate.gainItem(true);
                    }
                    else
                        Debug.Log(alreadyHere.name);

                }
                if (gamestate.getState() == gameState.state.GREEN)
                {

                    if (alreadyHere == null)
                    {
                        if (gamestate.altEnemiesLeft > 0)
                        {
                            Instantiate(Resources.Load("altBugs"), hiliteSprite.transform.position + new Vector3(0, 1.6f, 0), hiliteSprite.transform.rotation);
                            gamestate.spendEnemy(true);
                        }
                    }
                    else if (alreadyHere.name.StartsWith("altBugs"))
                    {
                        Destroy(alreadyHere);
                        gamestate.gainEnemy(true);
                    }
                }
            }
        }
    }

}
