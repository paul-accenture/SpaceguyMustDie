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
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate.inputEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int coordinate = gridLayout.WorldToCell(mouseWorldPos);

                //if (getTileAtPosition(coordinate) != null)
                hiliteSprite.transform.SetPositionAndRotation(gridLayout.CellToWorld(coordinate) + offset, new Quaternion());

                //}

                if (getActiveTile() != null && gamestate.getState() == gameState.state.RED)
                {
                    GameObject alreadyHere = getObjectAtActiveTile();

                    if (alreadyHere == null)
                    {
                        if (gamestate.jumpsLeft > 0)
                        {
                            Instantiate(Resources.Load("jumpBoard"), hiliteSprite.transform.position + new Vector3(0, (float).7, 0), hiliteSprite.transform.rotation);
                            gamestate.spendJump();
                        }
                    }
                    else if (alreadyHere.name.StartsWith("jumpBoard"))
                    {
                        Debug.Log(alreadyHere.name);
                        Destroy(alreadyHere);
                        gamestate.gainJump();
                    }
                    else
                        Debug.Log(alreadyHere.name);

                }

                if (getActiveTile() != null && gamestate.getState() == gameState.state.GREEN)
                {
                    GameObject alreadyHere = getObjectAtActiveTile();
                    if (alreadyHere == null)
                    {
                        if (gamestate.enemiesLeft > 0)
                        {
                            Instantiate(Resources.Load("bug"), hiliteSprite.transform.position + new Vector3(0, (float).8, 0), hiliteSprite.transform.rotation);
                            gamestate.spendEnemy();
                        }
                    }
                    else if (alreadyHere.name.StartsWith("bug"))
                    {
                        Destroy(alreadyHere);
                        gamestate.gainEnemy();
                    }
                }
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

    GameObject getObjectAtActiveTile()
    {
        Vector2 coordinate = new Vector2((hiliteSprite.transform.position).x, (hiliteSprite.transform.position).y);
        Collider2D[] hits = Physics2D.OverlapCircleAll(coordinate, 0.4f);
        foreach (Collider2D i in hits)
        {
            if (i.gameObject.name != "Tilemap")
            {
               
                return i.gameObject;
            }
        }
        return null;
    }

}
