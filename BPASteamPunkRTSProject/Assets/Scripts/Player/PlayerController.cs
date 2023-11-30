using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MapMoveSens = 1f;
    public float MapZoomSens = 1f;
    public int Inverse = 1;
    public float MapScrollSens = 1;
    public Vector3 PreviousPos;
    public List<GameObject> selectedUnits = new List<GameObject>();
    public GameObject[,,] map;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator MoveOverTime(List<TileInMemory> MoveList, GameObject[,,] map, Unit unit)
    {


        foreach (TileInMemory item in MoveList)
        {
            while (unit.transform.position != map[item.x, item.y, item.layer].transform.position)
            {
                unit.transform.position = Vector3.MoveTowards(unit.transform.position, map[item.x, item.y, item.layer].transform.position, unit.moveSpeed * Time.deltaTime);
                yield return null;
            }
            unit.position = new int[] { item.x, item.y, item.layer };
            unit.positionAsVector3 = new Vector3(item.x, item.y, item.layer);
        }
        //MoveList2 = MoveList;
        //IsMoving = false;

    }
    // Update is called once per frame
    void Update()
    {
        //left click check
        //Uncomment the following if you want to work on exprimental A* pahtfinding.

        if (Input.GetMouseButtonDown(1))
        {
            MoveUnit();
        }
        if (Input.GetMouseButtonDown(0))
        {
            //When creating code that you want run on left click, please create an appropriately named method, that way we don't
            //have to wade through your code to find our code.
            //Make sure the method can be called in any order relative to the other methods, that way nothing glitches out that we can't figure out.
            Select();
        }
        MoveMap();
    }
    void MoveUnit()
    {
        if (selectedUnits.Count > 0)
        {
            var hits = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(0, 0));
            GameObject SelectedObject = null;
            Vector2 destination;
            //takes the array value of the hex that has been selected, and converts it to it's real world position.
            destination = new Vector2(hits[0].transform.position.x, hits[0].transform.position.y);

            foreach (GameObject unit in selectedUnits)
            {
                bool[,,] mapForPathwayPurposes = new bool[map.GetLength(0), map.GetLength(1), map.GetLength(2)];
                for(int x = 0; x < mapForPathwayPurposes.GetLength(0); x++)
                {
                    for (int y = 0; y < mapForPathwayPurposes.GetLength(1); y++)
                    {
                        for (int z = 0; z < mapForPathwayPurposes.GetLength(2); z++)
                        {
                            mapForPathwayPurposes[x, y, z] = map[x, y, z].GetComponent<Tile>().Moveable;
                        }
                    }
                }
                var location = unit.transform.position;
                List<TileInMemory> storedTiles = new List<TileInMemory>();
                List<TileInMemory> potentialTiles = new List<TileInMemory>();
                List<TileInMemory> movetiles = new List<TileInMemory>();
                int[] position = unit.GetComponent<Unit>().position;
                int[] prevPosition = null;
                //potentialTiles.Add(new TileInMemory(1, 0, null, position[0], position[1], position[2]));
                float time = Time.time;
                while ((position[0] != hits[0].transform.GetComponent<Tile>().position[0]) || (position[1] != hits[0].transform.GetComponent<Tile>().position[1]))
                {
                    //if psoition[0] is 0, it can only do east, north west, north east, west, 
                    List<TileInMemory> theTiles = new List<TileInMemory>();
                    //check for all tiles surrounding current
                    //var position = new int[] { position[0] + 1, position[1], position[2] };
                    //east
                    float g = Vector3.Distance(destination, map[position[0] + 1, position[1], position[2]].transform.position);
                    theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] + 1, position[1], position[2]));
                    //even y axis
                    if (Mathf.RoundToInt((2f * (float)(((float)position[1] / 2f) - (int)(position[1] / 2)))) != 1)
                    {
                        //north east if even y axis
                        g = Vector3.Distance(destination, map[position[0] + 1, position[1] + 1, position[2]].transform.position);
                        theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] + 1, position[1] + 1, position[2]));
                        //                    //north west

                        g = Vector3.Distance(destination, map[position[0], position[1] + 1, position[2]].transform.position);
                        theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] + 1, position[2]));

                        if (position[1] > 0)
                        {
                            //                    //south west if even y axis 
                            g = Vector3.Distance(destination, map[position[0], position[1] - 1, position[2]].transform.position);
                            theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] - 1, position[2]));

                        }
                        if (position[1] > 0)
                        {
                            //                    // southeast 
                            g = Vector3.Distance(destination, map[position[0] + 1, position[1] - 1, position[2]].transform.position);
                            theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] + 1, position[1] - 1, position[2]));
                        }
                    }// odd y axis
                    else
                    {
                        //north east
                        g = Vector3.Distance(destination, map[position[0], position[1] + 1, position[2]].transform.position);
                        theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] + 1, position[2]));
                        if (position[0] > 0)
                        {
                            //                    //north west

                            g = Vector3.Distance(destination, map[position[0] - 1, position[1] + 1, position[2]].transform.position);
                            theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] - 1, position[1] + 1, position[2]));

                        }

                        if (position[1] > 0 && position[0] > 0)
                        {
                            //                    //south west 
                            g = Vector3.Distance(destination, map[position[0] - 1, position[1] - 1, position[2]].transform.position);
                            theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] - 1, position[1] - 1, position[2]));

                        }
                        if (position[1] > 0)
                        {
                            //                    // southeast if odd
                            g = Vector3.Distance(destination, map[position[0], position[1] - 1, position[2]].transform.position);
                            theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] - 1, position[2]));
                        }
                    }
                    if (position[0] > 0)
                    {
                        //west
                        g = Vector3.Distance(destination, map[position[0] - 1, position[1], position[2]].transform.position);
                        theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] - 1, position[1], position[2]));

                    }
                    //=                    // simple list should allow all potential tiles to be organized by h value and checked every time. 
                    if (prevPosition != null)
                    {
                        foreach (TileInMemory tile in theTiles)
                        {
                            if (tile.x == prevPosition[0] && tile.y == prevPosition[1])
                            {

                            }
                            else
                            {
                                TileInMemory newTile = storeTilesIfCheck(tile, theTiles, mapForPathwayPurposes);
                                if (newTile != null)
                                {
                                    storedTiles.Add(newTile);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (TileInMemory tile in theTiles)
                        {
                           TileInMemory newTile = storeTilesIfCheck(tile, theTiles, mapForPathwayPurposes);
                            if (newTile != null)
                            {
                                storedTiles.Add(newTile);
                            }
                            }
                    }
                    storedTiles.Sort();
                    potentialTiles.Add(storedTiles[0]);
                    storedTiles.Remove(storedTiles[0]);
                    prevPosition = position;
                    position = new int[] { potentialTiles[potentialTiles.Count - 1].x, potentialTiles[potentialTiles.Count - 1].y, potentialTiles[potentialTiles.Count - 1].layer };
                    if (Time.time - time > 1)
                    {
                        return;
                    }
                }
                movetiles.Add(potentialTiles[potentialTiles.Count - 1]);
                while (movetiles[movetiles.Count - 1].filllocationAsVector3 != unit.GetComponent<Unit>().positionAsVector3)
                {
                    //movetiles.Add(potentialTiles.Find(x => x.locationAsArr == movetiles[movetiles.Count-1].fillLocation));
                    foreach (TileInMemory tile in potentialTiles)
                    {
                        if (tile.locationAsVector3 == movetiles[movetiles.Count - 1].filllocationAsVector3)
                        {
                            movetiles.Add(tile);
                        }
                    }
                }
                movetiles.Reverse();
                StartCoroutine(MoveOverTime(movetiles, map, unit.GetComponent<Unit>()));
            }
        }
    }
    TileInMemory storeTilesIfCheck(TileInMemory tile, List<TileInMemory> storedTiles, bool[,,] tempMap)
    {
         if (tempMap[tile.x, tile.y, tile.layer] == true)
               {
                tempMap[tile.x, tile.y, tile.layer] = false;
            return tile;
                }
 
        return null;

    }
    void Select()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        GameObject SelectedObject = null;
        foreach (RaycastHit hit in hits)
        {
            if (!selectedUnits.Contains(hit.transform.gameObject))
            {
                SelectedObject = hit.transform.gameObject;
            }
        }
        if (SelectedObject != null)
        {
            TrySelect(SelectedObject);
        }
    }
    void TrySelect(GameObject hit)
    {
        if(hit != null)
        {
            selectedUnits.Add(hit);
            hit.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
        }
    }
    void MoveMap()
    {
               Vector3 map = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            map += new Vector3(-1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            map += new Vector3(1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            map += new Vector3(0f, 1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            map += new Vector3(0f, -1, 0);
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            var zoomAmount = (Input.mouseScrollDelta.y * MapZoomSens * -1);
            if (zoomAmount + Camera.FindObjectOfType<Camera>().orthographicSize > .1)
            {
                Vector3 topright = new Vector3(Screen.width, Screen.height, -10);
                Vector3 topleft = new Vector3(0, 0, -10);
                topright = Camera.main.ScreenToWorldPoint(topright) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // 1,1 -1,-1
                //becomes 
                topleft = Camera.main.ScreenToWorldPoint(topleft) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                topright = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (topright * ((Camera.FindObjectOfType<Camera>().orthographicSize + zoomAmount) / Camera.FindObjectOfType<Camera>().orthographicSize));
                //becomes 
                topleft = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (topleft * ((Camera.FindObjectOfType<Camera>().orthographicSize + zoomAmount) / Camera.FindObjectOfType<Camera>().orthographicSize));
                Camera.main.transform.position = new Vector3((topleft.x + topright.x) / 2, (topleft.y + topright.y) / 2, -10);
                Camera.FindObjectOfType<Camera>().orthographicSize += zoomAmount;
            }
            //1. Determine what the scale factor is for the top right and bottom left
            //2. Scale it, then average the x and y for the camera transform position

            //scrolling for different directions
            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 diff = (pos - Camera.main.transform.position) ;
            //Vector3 movement = new Vector3(0, 0, 0);
            //    movement += new Vector3(Math.Max(Math.Abs(diff.x), MapScrollSens) * (diff.x / Math.Abs(diff.x)) * Time.deltaTime, Math.Max(Math.Abs(diff.y), MapScrollSens) * (diff.x / Math.Abs(diff.x)) * Time.deltaTime, 0);

            //Camera.main.transform.position = new Vector3(movement.x, movement.y, 0) +  Camera.main.transform.position;

        }
        if (PreviousPos == map)
        {

        }
        else
        {
            Camera.FindObjectOfType<Camera>().transform.position += map * MapMoveSens * Time.deltaTime;

            //Vector3 movement = PreviousPos - selectPos;
            //Camera.FindObjectOfType<Camera>().transform.position += (movement * Inverse) * MapMoveSens;
            //PreviousPos = selectPos + (movement * Inverse);
        }
    }
}
