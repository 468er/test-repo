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
    public  List<GameObject> movetiles2 = new List<GameObject>();
    public List<ONETWOTHREE> moveOrders = new List<ONETWOTHREE>();
    // Start is called before the first frame update
    void Start()
    {

    }
   
    // Update is called once per frame

    void PrepareMoveOrders(Vector3 destination, RaycastHit2D[] hits, GameObject unit)
    {
        LT_G3_U moveprepared = null;
        //if the layer distance is greater than one, just try to get to the next layer
        for(int a = 0; a < Mathf.Abs(destination.z - unit.GetComponent<Unit>().position[2]) - 1; a ++)
        {
            moveOrders.Add(MoveUnit(destination, hits[0].transform.GetComponent<Tile>(), unit, false));
        }
        if(moveOrders.Count > 0)
        {
            int count = moveOrders.Count;
            int potentialTilesCount = moveOrders[count - 1].PotentialTiles.Count;
            TileInMemory last = moveOrders[count -1].PotentialTiles[potentialTilesCount - 1];
            GameObject tile = map[last.x, last.y, last.layer];
            moveOrders.Add(MoveUnit(destination, hits[0].transform.GetComponent<Tile>(), tile, true));
        }
        else
        {
            moveOrders.Add(MoveUnit(destination, hits[0].transform.GetComponent<Tile>(), unit, true));
        }
        List<TileInMemory> newLst = new List<TileInMemory>();
        moveOrders.Reverse();
        foreach (ONETWOTHREE item in moveOrders)
        {
            moveprepared = PrepareMove(item.PotentialTiles, unit);
            moveprepared.MoveList1.Reverse();
            foreach(TileInMemory item2 in moveprepared.MoveList1)
            {
                newLst.Add(item2);

            }
        }
        if(moveprepared != null)
        {
            moveOrders.Clear();
            newLst.Reverse();
            StartCoroutine(unit.GetComponent<Unit>().MoveOverTime(newLst, moveprepared.map1, moveprepared.unit1));
        }
    }
    void Update()
    {
        //left click check
        //Uncomment the following if you want to work on exprimental A* pahtfinding.

        if (Input.GetMouseButtonDown(1))
        {
            if (selectedUnits.Count > 0)
            {
                var hits = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(0, 0));
                GameObject SelectedObject = null;
                Vector3 destination;
                //takes the array value of the hex that has been selected, and converts it to it's real world position.
                destination = new Vector3(hits[0].transform.position.x, hits[0].transform.position.y / 0.86602540378443864676372317075294f, hits[0].transform.GetComponent<Tile>().position[2]);
                foreach (GameObject unit in selectedUnits)
                {
                     PrepareMoveOrders(destination, hits, unit);

                }
            }
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
    ONETWOTHREE MoveUnit(Vector3 destination, Tile destinationTile, GameObject unit, bool normal)
    {

        Vector3 destinationAsRealPosition = new Vector3(destination.x, destination.y * 0.86602540378443864676372317075294f, destination.z);
        //
        //new class tunnel
        //tunnel contains id, all squares, all layers covered.
        //h value becomes distance from the entrance of the tunnel + length of tunnel + distance from exit of tunnel to destination.
        bool[,,] mapForPathwayPurposes = new bool[map.GetLength(0), map.GetLength(1), map.GetLength(2)];
        for (int x = 0; x < mapForPathwayPurposes.GetLength(0); x++)
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
        int[] position;
        if (unit.GetComponent<Unit>() != null)
        {
            position = unit.GetComponent<Unit>().position;
        }
        else {
           position = unit.GetComponent<Tile>().position;
        }
        int[] prevPosition = position;
        //potentialTiles.Add(new TileInMemory(1, 0, null, position[0], position[1], position[2]));
        float time = Time.time;
        List<TunnelInMemory> ReOrganized = new List<TunnelInMemory>();
        bool endLoopConditions = false;
        //have a list of tunnels
        //loop through this list of tunnels to find the shortest path every time
        //how to find the path length?
        //step 1: Start with tunnels on this layer. Start with first tunnel. Layer exit of that tunnel, search for the first tunnel. Check that one. Each time, check if the exit of the tunnel is on the level of the tunnel exit
        //if we go back a layer on the exit, just cancel it there. 
        //would only actually have to reorganize the list every time a new layer is entered.

        //start from end tunnel, find closest tunnel.
        //if closest tunnel doesn't end on starting layer, find tunnel that goes down until at exit layer
        //store all of these in an array and 
        while (endLoopConditions == false)
        {
            //start of new code for multiple tunnels
            int numToSubtractForAllTileLayersNeeded = destinationTile.position[2] - position[2];
            //this finds all the layers in tunnels
            //List < List < Tunnel >> ListofTunnels = new List<List<Tunnel>>();
            //List<Tunnel> Path = new List<Tunnel>();
            //ListofTunnels = AllTunnelPathsToExit(Path, numToSubtractForAllTileLayersNeeded, position[2]);
           
            bool IsGettingFarther = false;
            //if psoition[0] is 0, it can only do east, north west, north east, west, 
            List<TileInMemory> theTiles = new List<TileInMemory>();
            //check for all tiles surrounding current
            //var position = new int[] { position[0] + 1, position[1], position[2] };
            bool XTooBig;
            bool YTooBig;
            if (position[0] + 1 >= map.GetLength(0))
            {
                XTooBig = true;
            }
            else
            {
                XTooBig = false;
            }
            if (position[1] + 1 >= map.GetLength(1))
            {
                YTooBig = true;
            }
            else
            {
                YTooBig = false;
            }
            
            //east
            float g;
            float z;
            if (!XTooBig)
            {
                 g = Vector3.Distance(destination, map[position[0] + 1, position[1], position[2]].transform.position);
                 z = Vector3.Distance(unit.transform.position, map[position[0] + 1, position[1], position[2]].transform.position);
                theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] + 1, position[1], position[2], z));

            }

            //even y axis

            if (Mathf.RoundToInt((2f * (float)(((float)position[1] / 2f) - (int)(position[1] / 2)))) != 1)
            {

                if(!XTooBig && !YTooBig)
                {
                    //north east if even y axis
                    g = Vector3.Distance(destination, map[position[0] + 1, position[1] + 1, position[2]].transform.position);
                    z = Vector3.Distance(unit.transform.position, map[position[0] + 1, position[1] + 1, position[2]].transform.position);
                    theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] + 1, position[1] + 1, position[2], z));

                }
                if (!YTooBig)
                {
                    //                    //north west

                    g = Vector3.Distance(destination, map[position[0], position[1] + 1, position[2]].transform.position);
                    z = Vector3.Distance(unit.transform.position, map[position[0], position[1] + 1, position[2]].transform.position);

                    theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] + 1, position[2], z));

                }
                if (position[1] > 0)
                {
                    //                    //south west if even y axis 
                    g = Vector3.Distance(destination, map[position[0], position[1] - 1, position[2]].transform.position);
                    z = Vector3.Distance(unit.transform.position, map[position[0], position[1] - 1, position[2]].transform.position);

                    theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] - 1, position[2], z));

                }
                if (position[1] > 0)
                {
                    //                    // southeast 
                    if (!XTooBig)
                    {
                        g = Vector3.Distance(destination, map[position[0] + 1, position[1] - 1, position[2]].transform.position);
                        z = Vector3.Distance(unit.transform.position, map[position[0] + 1, position[1] - 1, position[2]].transform.position);

                        theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] + 1, position[1] - 1, position[2], z));
                    }
                
                }
            }// odd y axis
            else
            {
                //north east
                if (!YTooBig)
                {
                    g = Vector3.Distance(destination, map[position[0], position[1] + 1, position[2]].transform.position);
                    z = Vector3.Distance(unit.transform.position, map[position[0], position[1] + 1, position[2]].transform.position);

                    theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] + 1, position[2], z));
                }
                //                    //north west
                if (position[0] > 0)
                {
                    if (!YTooBig)
                    {
                        g = Vector3.Distance(destination, map[position[0] - 1, position[1] + 1, position[2]].transform.position);
                        z = Vector3.Distance(unit.transform.position, map[position[0] - 1, position[1] + 1, position[2]].transform.position);

                        theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] - 1, position[1] + 1, position[2], z));
                    }
                   

                }
                //                    //south west 
                if (position[1] > 0 && position[0] > 0)
                {
                    g = Vector3.Distance(destination, map[position[0] - 1, position[1] - 1, position[2]].transform.position);
                    z = Vector3.Distance(unit.transform.position, map[position[0] - 1, position[1] - 1, position[2]].transform.position);

                    theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] - 1, position[1] - 1, position[2], z));

                }
                if (position[1] > 0)
                {
                    //                    // southeast if odd
                    g = Vector3.Distance(destination, map[position[0], position[1] - 1, position[2]].transform.position);
                    z = Vector3.Distance(unit.transform.position, map[position[0], position[1] - 1, position[2]].transform.position);

                    theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0], position[1] - 1, position[2], z));
                }
            }
            //west
            if (position[0] > 0)
            {
                g = Vector3.Distance(destination, map[position[0] - 1, position[1], position[2]].transform.position);
                z = Vector3.Distance(unit.transform.position, map[position[0] - 1, position[1], position[2]].transform.position);

                theTiles.Add(new TileInMemory(1, g, new int[] { position[0], position[1], position[2] }, position[0] - 1, position[1], position[2], z));

            }
            //=                    // simple list should allow all potential tiles to be organized by h value and checked every time. 
            //all iterations after first
            if (prevPosition != null)
            {

                if (position[2] != destination.z)
                {
                    Tile currentTile = map[position[0], position[1], position[2]].GetComponent<Tile>();
                    TunnelInMemory tunnel = ReOrganized.Find(x => x.Entrance == map[position[0], position[1], position[2]].GetComponent<Tile>());
                    if (tunnel != null)
                    {
                        storedTiles.Add(new TileInMemory(tunnel.length, tunnel.distanceFromDestination, tunnel.Entrance.position, tunnel.Exit.position[0], tunnel.Exit.position[1], tunnel.Exit.position[2], 0));

                    }
                }
                foreach (TileInMemory tile in theTiles)
                {
                    if (tile.x == prevPosition[0] && tile.y == prevPosition[1])
                    {

                    }
                    else
                    {
                        //if check to see if boolean map lines up
                        TileInMemory newTile = storeTilesIfCheck(tile, theTiles, mapForPathwayPurposes);
                        if (newTile != null)
                        {   //
                            TileInMemory newestTile = newTile;
                            if (position[2] != destination.z)
                            {
                                if (Mathf.Abs(destination.z - position[2]) > 1)
                                {
                                    int multiplicity = ((int)destination.z - position[2]) / (Mathf.Abs((int)destination.z - position[2]));
                                    List<Tile> entrancetiles = new List<Tile>();
                                    List<List<TileInMemory>> list = new List<List<TileInMemory>>();
                                    List<Tile> exits = new List<Tile>();
                                    //search for something that 
                                    foreach (Tunnel item in gameManager.Tunnels)
                                    {
                                        //if the tunnel contains a tile that leads to the exit layer
                                        if (item.layers.Contains(Mathf.RoundToInt(position[2] + (multiplicity))))
                                        {
                                            //if the tunnel contains a tile that leads to entrance layer
                                            List<Tile> entrance;
                                            if (unit.GetComponent<Unit>() != null)
                                            {
                                                entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Unit>().positionAsVector3.z);
                                            }
                                            else
                                            {
                                                entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Tile>().positionAsVector3.z);
                                            }
                                           
                                            exits = item.Tiles.FindAll(x => x.position[2] == position[2] + (multiplicity));
                                            for (int i = 0; i < exits.Count; i++)
                                            {
                                                exits[i].IsTunnelExit(exits[i], Vector2.Distance(exits[i].transform.position, exits[i].transform.position));
                                            }
                                            if (entrance.Count > 0 && exits.Count > 0)
                                            {
                                                ReOrganized.Clear();
                                                foreach (Tile Tile in entrance)
                                                {
                                                    entrancetiles.Add(Tile);
                                                }
                                                for (int x = 0; x < exits.Count; x++)
                                                {
                                                    for (int y = 0; y < entrance.Count; y++)
                                                    {
                                                        TunnelInMemory tun = new TunnelInMemory(item.id, entrance[y], exits[x], map[tile.x, tile.y, tile.layer].transform.position, exits[x].transform.position);
                                                        ReOrganized.Add(tun);
                                                    }
                                                }
                                                ReOrganized.Sort();
                                                newestTile = new TileInMemory(1, ReOrganized[0].h, new int[] { newTile.fillLocation[0], newTile.fillLocation[1], newTile.fillLocation[2] }, newTile.x, newTile.y, newTile.layer, newTile.z);
                                            }
                                            //for each tunnel, if it contains exit layer, an contains entrance layer, it needs to do all combinatiosn. So, I guess creating a new tunnel class with only one entrance and exit, length, and distance from dest is the way to go.
                                        }
                                    }
                                }
                                else
                                {
                                    List<Tile> entrancetiles = new List<Tile>();
                                    List<List<TileInMemory>> list = new List<List<TileInMemory>>();
                                    List<Tile> exits = new List<Tile>();
                                    //search for something that 
                                    foreach (Tunnel item in gameManager.Tunnels)
                                    {
                                        //if the tunnel contains a tile that leads to the exit layer
                                        if (item.layers.Contains(Mathf.RoundToInt(destination.z)))
                                        {
                                            //if the tunnel contains a tile that leads to entrance layer
                                            List<Tile> entrance;
                                            if (unit.GetComponent<Unit>() != null)
                                            {
                                                entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Unit>().positionAsVector3.z);
                                            }
                                            else
                                            {
                                                entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Tile>().positionAsVector3.z);
                                            }
                                            exits = item.Tiles.FindAll(x => x.position[2] == destination.z);
                                            for (int i = 0; i < exits.Count; i++)
                                            {
                                                exits[i].IsTunnelExit(exits[i], Vector2.Distance(exits[i].transform.position, destinationAsRealPosition));
                                            }
                                            if (entrance.Count > 0)
                                            {
                                                ReOrganized.Clear();
                                                foreach (Tile Tile in entrance)
                                                {
                                                    entrancetiles.Add(Tile);
                                                }
                                                for (int x = 0; x < exits.Count; x++)
                                                {
                                                    for (int y = 0; y < entrance.Count; y++)
                                                    {
                                                        TunnelInMemory tun = new TunnelInMemory(item.id, entrance[y], exits[x], map[tile.x, tile.y, tile.layer].transform.position, destinationAsRealPosition);
                                                        ReOrganized.Add(tun);
                                                    }
                                                }
                                                ReOrganized.Sort();
                                                newestTile = new TileInMemory(1, ReOrganized[0].h, new int[] { newTile.fillLocation[0], newTile.fillLocation[1], newTile.fillLocation[2] }, newTile.x, newTile.y, newTile.layer, newTile.z);
                                            }
                                            //for each tunnel, if it contains exit layer, an contains entrance layer, it needs to do all combinatiosn. So, I guess creating a new tunnel class with only one entrance and exit, length, and distance from dest is the way to go.
                                        }
                                    }
                                }

                            }
                          
                            storedTiles.Add(newestTile);
                            //potentialTiles[potentialTiles.Count - 1].children.Add(newTile);
                        }
                    }
                }
            }//first iteration
            else
            {
                if (position[2] != destination.z)
                {
                    Tile currentTile = map[position[0], position[1], position[2]].GetComponent<Tile>();
                    TunnelInMemory tunnel = ReOrganized.Find(x => x.Entrance == map[position[0], position[1], position[2]].GetComponent<Tile>());
                    if (map[position[0], position[1], position[2]].GetComponent<Tile>().Tunnel != 0)
                    {
                        storedTiles.Add(new TileInMemory(tunnel.length, tunnel.distanceFromDestination, tunnel.Entrance.position, tunnel.Exit.position[0], tunnel.Exit.position[1], tunnel.Exit.position[2], 0));
    
                    }
                }
                foreach (TileInMemory tile in theTiles)
                {
                    TileInMemory newTile = storeTilesIfCheck(tile, theTiles, mapForPathwayPurposes);
                    if (newTile != null)
                    {   // 
                        TileInMemory newestTile = newTile;
                        if (position[2] != destination.z)
                        {
                            if (Mathf.Abs(destination.z - position[2]) > 1)
                            {
                                int multiplicity = ((int)destination.z - position[2]) / (Mathf.Abs((int)destination.z - position[2]));
                                List<Tile> entrancetiles = new List<Tile>();
                                List<List<TileInMemory>> list = new List<List<TileInMemory>>();
                                List<Tile> exits = new List<Tile>();
                                //search for something that 
                                foreach (Tunnel item in gameManager.Tunnels)
                                {
                                    //if the tunnel contains a tile that leads to the exit layer
                                    if (item.layers.Contains(Mathf.RoundToInt(position[2] + (multiplicity))))
                                    {
                                        //if the tunnel contains a tile that leads to entrance layer
                                        List<Tile> entrance;
                                        if (unit.GetComponent<Unit>() != null)
                                        {
                                            entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Unit>().positionAsVector3.z);
                                        }
                                        else
                                        {
                                            entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Tile>().positionAsVector3.z);
                                        }

                                        exits = item.Tiles.FindAll(x => x.position[2] == position[2] + (multiplicity));
                                        for (int i = 0; i < exits.Count; i++)
                                        {
                                            exits[i].IsTunnelExit(exits[i], Vector2.Distance(exits[i].transform.position, exits[i].transform.position));
                                        }
                                        if (entrance.Count > 0 && exits.Count > 0)
                                        {
                                            ReOrganized.Clear();
                                            foreach (Tile Tile in entrance)
                                            {
                                                entrancetiles.Add(Tile);
                                            }
                                            for (int x = 0; x < exits.Count; x++)
                                            {
                                                for (int y = 0; y < entrance.Count; y++)
                                                {
                                                    TunnelInMemory tun = new TunnelInMemory(item.id, entrance[y], exits[x], map[tile.x, tile.y, tile.layer].transform.position, exits[x].transform.position);
                                                    ReOrganized.Add(tun);
                                                }
                                            }
                                            ReOrganized.Sort();
                                            newestTile = new TileInMemory(1, ReOrganized[0].h, new int[] { newTile.fillLocation[0], newTile.fillLocation[1], newTile.fillLocation[2] }, newTile.x, newTile.y, newTile.layer, newTile.z);
                                        }
                                        //for each tunnel, if it contains exit layer, an contains entrance layer, it needs to do all combinatiosn. So, I guess creating a new tunnel class with only one entrance and exit, length, and distance from dest is the way to go.
                                    }
                                }
                            }
                            else
                            {
                                List<Tile> entrancetiles = new List<Tile>();
                                List<List<TileInMemory>> list = new List<List<TileInMemory>>();
                                List<Tile> exits = new List<Tile>();
                                //search for something that 
                                foreach (Tunnel item in gameManager.Tunnels)
                                {
                                    //if the tunnel contains a tile that leads to the exit layer
                                    if (item.layers.Contains(Mathf.RoundToInt(destination.z)))
                                    {
                                        //if the tunnel contains a tile that leads to entrance layer
                                        List<Tile> entrance;
                                        if (unit.GetComponent<Unit>() != null)
                                        {
                                            entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Unit>().positionAsVector3.z);
                                        }
                                        else
                                        {
                                            entrance = item.Tiles.FindAll(x => x.position[2] == unit.GetComponent<Tile>().positionAsVector3.z);
                                        }
                                        exits = item.Tiles.FindAll(x => x.position[2] == destination.z);
                                        for (int i = 0; i < exits.Count; i++)
                                        {
                                            exits[i].IsTunnelExit(exits[i], Vector2.Distance(exits[i].transform.position, destination));
                                        }
                                        if (entrance.Count > 0)
                                        {
                                            ReOrganized.Clear();
                                            foreach (Tile Tile in entrance)
                                            {
                                                entrancetiles.Add(Tile);
                                            }
                                            for (int x = 0; x < exits.Count; x++)
                                            {
                                                for (int y = 0; y < entrance.Count; y++)
                                                {
                                                    TunnelInMemory tun = new TunnelInMemory(item.id, entrance[y], exits[x], unit.transform.position, destinationAsRealPosition);
                                                    ReOrganized.Add(tun);
                                                }
                                            }
                                            ReOrganized.Sort();
                                            newestTile = new TileInMemory(1, ReOrganized[0].h, new int[] { newTile.fillLocation[0], newTile.fillLocation[1], newTile.fillLocation[2] }, newTile.x, newTile.y, newTile.layer, newTile.z);
                                        }
                                        //for each tunnel, if it contains exit layer, an contains entrance layer, it needs to do all combinatiosn. So, I guess creating a new tunnel class with only one entrance and exit, length, and distance from dest is the way to go.
                                    }
                                }
                            }
                            
                        }
                        storedTiles.Add(newestTile);
                        //potentialTiles[potentialTiles.Count - 1].children.Add(newTile);
                    }
                }
            }
            storedTiles.Sort();
            potentialTiles.Add(storedTiles[0]);
            //     if (IsGettingFarther == true)
            //     {
            ////by checking if the current tile's h value is higher than the last one, we know whether or not it is getting closer/farther.
            //        if (potentialTiles[potentialTiles.Count -1].h > potentialTiles.Find(x => x.locationAsVector3 == potentialTiles[potentialTiles.Count - 1].filllocationAsVector3).h + .01f)
            //        {

            //        }
            //         else
            //           {
            //          IsGettingFarther = false;
            //         TileInMemory tile = potentialTiles[potentialTiles.Count - 1];
            //         ONETWOTHREE TheObject = MoveUnit(new Vector2(map[tile.x, tile.y, tile.layer].transform.position.x, map[tile.x, tile.y, tile.layer].transform.position.y), map[tile.x, tile.y, tile.layer].GetComponent<Tile>(), unit);
            //         potentialTiles = TheObject.PotentialTiles;
            //         potentialTiles = TheObject.StoredTiles;
            //            mapForPathwayPurposes = TheObject.walkPath;
            //       }
            //  }
            //  else
            //  {
            //      TileInMemory newTile = potentialTiles[potentialTiles.Count - 1];
            //     if (potentialTiles.Find(x => x.locationAsVector3 == newTile.filllocationAsVector3) != null)
            //        {
            //            if (newTile.h > potentialTiles.Find(x => x.locationAsVector3 == newTile.filllocationAsVector3).h + .01f)
            //           {
            //               IsGettingFarther = true;
            //           }
            //      }

            //    }
            storedTiles.Remove(storedTiles[0]);
            prevPosition = position;
            position = new int[] { potentialTiles[potentialTiles.Count - 1].x, potentialTiles[potentialTiles.Count - 1].y, potentialTiles[potentialTiles.Count - 1].layer };
            if (normal == false && prevPosition[2] != position[2])
            {
                endLoopConditions = true;
            }
            else
            {
                
                if((position[0] == destinationTile.position[0]) && (position[1] == destinationTile.position[1]) && (position[2] == destinationTile.position[2]))
                {
                    endLoopConditions = true;
                }
            }
                

        }
        return new ONETWOTHREE(storedTiles, potentialTiles, mapForPathwayPurposes);

    }
    //List<TunnelList> AllTunnelPathsToExit(TunnelList Path, int AllTunnelLayers, int cl)
    //{
        
    //    int multiplicity = (AllTunnelLayers - cl )/ (Mathf.Abs(AllTunnelLayers - cl));
    //    List<TunnelList> returnvar = new List<TunnelList>();
    //    TunnelList TunnelList = gameManager.Tunnels.FindAll(x => x.layers.Contains(cl) && x.layers.Contains(cl + multiplicity));
    //    for (int a = 0; a < TunnelList.Count; a++)
    //    {
    //        Path.tunnels.Add(TunnelList.tunnels[a]);
    //        if (0 < Mathf.Abs( AllTunnelLayers - cl))
    //        {
    //            returnvar = AllTunnelPathsToExit( Path,  AllTunnelLayers,  cl + multiplicity);
    //        }
    //        else
    //        {
    //            returnvar.Add(Path);
    //        }
    //    }
    //    return returnvar;
    //}
    LT_G3_U PrepareMove(List<TileInMemory> potentialTiles, GameObject unit)
    {
        LT_G3_U returnvar; 
        List<TileInMemory> movetiles = new List<TileInMemory>();

        movetiles.Add(potentialTiles[potentialTiles.Count - 1]);
        while (movetiles[movetiles.Count - 1].filllocationAsVector3 != potentialTiles[0].filllocationAsVector3)
        {
            //movetiles.Add(potentialTiles.Find(x => x.locationAsArr == movetiles[movetiles.Count-1].fillLocation));
            foreach (TileInMemory tile in potentialTiles)
            {
                if (tile.locationAsVector3 == movetiles[movetiles.Count - 1].filllocationAsVector3)
                {
                    //if()
                    movetiles.Add(tile);
                }
            }
        }
        movetiles.Reverse();
        foreach( TileInMemory tile in movetiles)
        {
            movetiles2.Add(map[(int)tile.locationAsVector3.x, (int)tile.locationAsVector3.y, (int)tile.locationAsVector3.z]);
        }
        returnvar = new LT_G3_U(movetiles, map, unit.GetComponent<Unit>());
        return returnvar;
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
        if (hit != null)
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
