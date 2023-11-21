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
        map = gameManager.map; 
    }

    // Update is called once per frame
    void Update()
    {
        //left click check
        //Uncomment the following if you want to work on exprimental A* pahtfinding.
       /* if (Input.GetMouseButtonDown(1))
        {
            if (selectedUnits.Count > 0)
            {
                foreach (GameObject unit in selectedUnits)
                {
                    var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var location = unit.transform.position;
                    List<TileInMemory> storedTiles = new List<TileInMemory>();
                    List<GameObject> potentialTiles = new List<GameObject>();
                    int[] position = unit.GetComponent<Unit>().position;
                    potentialTiles.Add(map[unit.GetComponent<Unit>().position[0], unit.GetComponent<Unit>().position[1], unit.GetComponent<Unit>().position[2]]);
                    //check for all tiles surrounding current
                    //east
                    float g = Vector3.Distance(destination, map[position[0]+1, position[1], position[2]].transform.position);
                    storedTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1] }));        
                    //north west
                     g = Vector3.Distance(destination, map[position[0], position[1]+1, position[2]].transform.position);
                    storedTiles.Add(new TileInMemory(1, g));         
                    //north east
                    g = Vector3.Distance(destination, map[position[0]+1, position[1]+1, position[2]].transform.position);
                    storedTiles.Add(new TileInMemory(1, g));    
                    //west
                    g = Vector3.Distance(destination, map[position[0], position[1]-1, position[2]].transform.position);
                    storedTiles.Add(new TileInMemory(1, g));
                    //south east
                    g = Vector3.Distance(destination, map[position[0]+1, position[1]-1, position[2]].transform.position);
                    storedTiles.Add(new TileInMemory(1, g));
                    //south west
                    g = Vector3.Distance(destination, map[position[0]-1, position[1]-1, position[2]].transform.position);
                    storedTiles.Add(new TileInMemory(1, g));
=                    // simple list should allow all potential tiles to be organized by h value and checked every time. 
                        storedTiles.Sort();
                    //then check the first, add it to potential tiles, check for all the tiles around it, repeat.
                }
            }
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            //When creating code that you want run on left click, please create an appropriately named method, that way we don't
            //have to wade through your code to find our code.
            //Make sure the method can be called in any order relative to the other methods, that way nothing glitches out that we can't figure out.
            Select();
        }
        MoveMap();
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
