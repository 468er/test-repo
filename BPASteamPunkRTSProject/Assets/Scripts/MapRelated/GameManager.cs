using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int x;
    public int y;
    public int layers;
    public GameObject tile;
    public GameObject layer;
    public GameObject[,,] map;
    public PlayerController Player1;
    public List<Tile> Tunnels = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[x, y, layers];
        for (int w = 0; w < layers; w++)
        {
            GameObject newLayer = Instantiate(layer);
            for (int a = 0; a < x; a++)
            {
                for (int b = 0; b < y; b++)
                {
                    GameObject newObj = Instantiate(tile, new Vector3(a + -1 * (((float)b / 2f) - (int)(b / 2)), b * 0.86602540378443864676372317075294f, 0), Quaternion.identity, newLayer.transform);
                    map[a, b, w] = (newObj);
                    newObj.name = "" + a + b + w;
                    newObj.GetComponent<Tile>().position = new int[] { a, b, w };
                }
            }
            newLayer.transform.position = new Vector3(x * w + w, 0, 0);
        }
        map[2, 2, 0].GetComponent<Tile>().IsTunnel(1);
        map[2, 2, 1].GetComponent<Tile>().IsTunnel(1);
        Tunnels.Add(map[2, 2, 1].GetComponent<Tile>());
        Tunnels.Add(map[2, 2, 0].GetComponent<Tile>());
        Player1.map = map;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
