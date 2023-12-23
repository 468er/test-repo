using UnityEngine;

public class Tile : MonoBehaviour
{
    public int[] position;
    public bool Moveable;
    public int Tunnel;
    public float distancefromExit;
    public Vector3 positionAsVector3;
    public void IsTunnel(int i, GameManager gameManager)
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(.3f, 0, 0);
        Tunnel = i;
        bool GcontainsThisTunnel = false;
        foreach(Tunnel item in gameManager.Tunnels)
        {
            if(item.id == i)
            {
                GcontainsThisTunnel = true;
            }
        }
        if (GcontainsThisTunnel)
        {
            gameManager.Tunnels.Find(x => x.id == i).Tiles.Add(this);
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
    private void Start()
    {
        positionAsVector3 = new Vector3(position[0], position[1], position[2]);
    }
}
