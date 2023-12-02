using UnityEngine;

public class Tile : MonoBehaviour
{
    public int[] position;
    public bool Moveable;
    public int Tunnel;
    public void IsTunnel(int i)
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(.3f, 0, 0);
        Tunnel = i;
    }
    // Start is called before the first frame update
}
