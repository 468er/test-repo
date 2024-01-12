using UnityEngine;
using System.Collections.Generic;
public class Tile : MonoBehaviour
{
    public TileType TypeOfTile;
    public int[] position;
    public bool Moveable;
    public int Tunnel;
    public float distancefromExit;
    public Vector3 positionAsVector3;
    //if 0 is not a spawner;
    public Tile Spawner;
    public GameManager gameManager;
    public float lastfired;
    public float fireInterval = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.gameObject + "collided with " + collision);
        switch(collision.transform.tag)
        {
            case "Building":
                Moveable = false;
                break; 
            case "ResourceDep":
                Moveable = false;
                break;
        }
    }
    public void IsTunnel(int i, GameManager gameManager)
    {
        //make it visible it's a tunnel
        transform.GetComponent<SpriteRenderer>().color = new Color(.3f, 0, 0);
        //make sure that the tunnel id and i are the same
        Tunnel = i;
        //set the bool to false to default becuase we check below
        bool GcontainsThisTunnel = false;
        //check each tunnel in game manager, if it contains a tunnel with the same id as this tile then there's no need to do anything and we set to true
        foreach (Tunnel item in gameManager.Tunnels)
        {
            if (item.id == i)
            {
                GcontainsThisTunnel = true;
            }
        }
        //double check the boolean, not necessary but easier for me to read because of the notation where it's false, we need to create a new tunnel
        if (GcontainsThisTunnel)
        {
            //we find the tunnel and add this tile to it's list of tiles
            gameManager.Tunnels.Find(x => x.id == i).Tiles.Add(this);
            //if the tile's record of the layers it's in doesn't include the current layer, we'll add this tunnel's layer to it.
            if (!gameManager.Tunnels.Find(x => x.id == i).layers.Contains(position[2]))
            {
                gameManager.Tunnels.Find(x => x.id == i).layers.Add(position[2]);
            }
        }
        else
        {

            gameManager.Tunnels.Add(new Tunnel(i, this));
        }
    }
    public void IsTunnelExit(Tile tile, float distance)
    {
        Moveable = tile.Moveable;
        position = tile.position;
        distancefromExit = distance;
    }
    // Start is called before the first frame update

    public void Initialize()
    {
        positionAsVector3 = new Vector3(position[0], position[1], position[2]);
        switch (TypeOfTile)
        {
            case TileType.wall:
                {
                    Moveable = false;
                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                    break;
                }
            case TileType.MagmaPool:
                {
                    Moveable = false;
                    GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
            case TileType.SpawnTrigger:
                {
                    Moveable = true;
                    GetComponent<SpriteRenderer>().color = Color.magenta;
                    break;
                }
            case TileType.Lava:
                {
                    Moveable = true;
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                }
            case TileType.nothing:
                {
                    Moveable = true;
                    GetComponent<SpriteRenderer>().color = Color.white;
                    break;
                }
            case TileType.Tunnel:
                {
                    Moveable = true;
                    IsTunnel(Tunnel, gameManager);
                    GameObject gameObject = Instantiate(gameManager.tilenNumberPrefab);
                    gameObject.GetComponent<UIToWorldPointUpdater>().ParentTile = this.gameObject;
                    break;
                }
        }
    }

    public void Ability(Unit triggerUnit)
    {
        switch (TypeOfTile)
        {
            case TileType.wall:
                {
                    break;
                }
            case TileType.MagmaPool:
                {
                    break;
                }
            case TileType.SpawnTrigger:
                {
                    if(lastfired + fireInterval < Time.time)
                    {
                        Spawner.SpawnUnits(triggerUnit);
                        fireInterval = float.PositiveInfinity;
                    }
                    break;
                }
            case TileType.Lava:
                {
                    break;
                }
            case TileType.nothing:
                {

                    break;
                }
            case TileType.Tunnel:
                {

                    break;
                }
        }

    }
    public void SpawnUnits(Unit unit)
    {
        int amount = 3;
        List<GameObject> Wave = new List<GameObject>();
        for(int i = 0; i < amount; i++)
        {
            Wave.Add(gameManager.SpawnPool[(int)Random.Range(0, gameManager.SpawnPool.Count - 1)]);
        }
        foreach( GameObject enemy in Wave)
        {
            GameObject instantiated = Instantiate(enemy, transform.position, Quaternion.identity);
            instantiated.GetComponent<Unit>().position = position;
            instantiated.GetComponent<Unit>().targets.Add(unit.gameObject); 
        }
    }

}
    public enum TileType
    {
        wall,
        MagmaPool,
        SpawnTrigger,
        Lava,
        nothing,
        Tunnel,
    }

