using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unit : MonoBehaviour
{
    public int[] position = new int[] { 0, 0, 0 };
    public Vector3 positionAsVector3;
    public List<TileInMemory> moveTiles = new List<TileInMemory>();
    public bool moving = false;
    public Coroutine movingRoutine;
    public TileInMemory lastTile;
    public GameObject transporter = null;

    public float moveSpeed = 5f;
    public float Health = 10;
    public float Damage = 10;
    public float Range = 10;
    public unit_Type _Type;

    public bool isEnemy = false;
    // Start is called before the first frame update
    public void Start()
    {
        positionAsVector3 = new Vector3(position[0], position[1], position[2]);
    }
    // Update is called once per frame
    public IEnumerator MoveOverTime(List<TileInMemory> MoveList, GameObject[,,] map, Unit unit)
    {
        moving = true;
        int a = 0;
        while (a < MoveList.Count)
        {
            moveTiles.Add(MoveList[a]);
            a++;
        }
        a = 0;
        while( 0 < moveTiles.Count)
        {
            if (Vector2.Distance(unit.transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position) > 1)
            {
                unit.transform.position = map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position;
            }
            else
            {
                while (unit.transform.position != map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position)
                {
                    unit.transform.position = Vector3.MoveTowards(unit.transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position, unit.moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            unit.position = new int[] { moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer };
            unit.positionAsVector3 = new Vector3(moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer);
            lastTile = moveTiles[a];
            moveTiles.Remove(moveTiles[a]);
        }
        moving = false;
        //foreach (TileInMemory MoveList[a] in MoveList)
        //{
           
        //}
        //MoveList2 = MoveList;
        //IsMoving = false;

    }
    public TileInMemory ClearForMovement()
    {
        StopCoroutine(movingRoutine);
        TileInMemory LastTile = lastTile;
        moveTiles.Clear();
        moving = false;
        return LastTile;
    }
}
public enum unit_Type
{
    soldier,
    
}
