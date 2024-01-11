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
    public GameObject[] allLayers;
    public PlayerController Player1;
    public List<Tunnel> Tunnels = new List<Tunnel>();

    public bool is_on_button_1 { get; internal set; }
    public bool is_on_button_2 { get; internal set; }
    public bool is_on_button_4 { get; internal set; }
    public bool is_on_button_3 { get; internal set; }
    public bool is_on_button_5 { get; internal set; }
    public GameObject Canvas;
    public List<GameObject> SpawnPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(true);

        map = new GameObject[x, y, layers];
        for (int w = 0; w < layers; w++)
        {
            for (int a = 0; a < x; a++)
            {
                for (int b = 0; b < y; b++)
                {
                    map[a, b, w] = allLayers[w].transform.GetChild((a * y) + b).gameObject;
                    map[a, b, w].GetComponent<Tile>().gameManager = this;
                    map[a, b, w].GetComponent<Tile>().Initialize();
                }
            }
        }
        //old map generation, now we use prebuilt map.

        //for (int w = 0; w < layers; w++)
        //{
        //    GameObject newLayer = Instantiate(layer);
        //    for (int a = 0; a < x; a++)
        //    {
        //        for (int b = 0; b < y; b++)
        //        {
        //            GameObject newObj = Instantiate(tile, new Vector3(a + -1 * (((float)b / 2f) - (int)(b / 2)), b * 0.86602540378443864676372317075294f, 0), Quaternion.identity, newLayer.transform);
        //            map[a, b, w] = (newObj);
        //            newObj.name = "" + a + b + w;
        //            newObj.GetComponent<Tile>().position = new int[] { a, b, w };
        //        }
        //    }
        //    newLayer.transform.position = new Vector3(x * w + w, 0, 0);
        //}
        //map[2, 2, 0].GetComponent<Tile>().IsTunnel(1, this);
        //map[2, 2, 1].GetComponent<Tile>().IsTunnel(1, this);
        //map[4, 4, 1].GetComponent<Tile>().IsTunnel(2, this);
        //map[4, 4, 2].GetComponent<Tile>().IsTunnel(2, this);
        Player1.map = map;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
