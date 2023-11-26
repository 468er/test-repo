using System.Collections;
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
                    GameObject newObj = Instantiate(tile, new Vector3(a + -1 *(((float)b / 2f) - (int)(b / 2)), b* 0.86602540378443864676372317075294f, 0), Quaternion.identity, newLayer.transform);
                    map[a,b,w] = (newObj);
                    newObj.name  = "" + a + b + w;
                    newObj.GetComponent<Tile>().position = new int[] { a, b, w };
                }
            }
            newLayer.transform.position = new Vector3(x * w + w, 0, 0);
        }
        Player1.map = map;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
