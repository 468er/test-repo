using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unit : MonoBehaviour
{
    public int[] position = new int[] { 0, 0, 0 };
    public Vector3 positionAsVector3;
    public float moveSpeed = 5f;
    public List<TileInMemory> moveTiles = new List<TileInMemory>();
    // Start is called before the first frame update
    void Start()
    {
        positionAsVector3 = new Vector3(position[0], position[1], position[2]);
    }

    // Update is called once per frame
    public IEnumerator MoveOverTime(List<TileInMemory> MoveList, GameObject[,,] map, Unit unit)
    {
        int a = 0;
        while (a < MoveList.Count)
        {
            moveTiles.Add(MoveList[a]);
            a++;
        }
        a = 0;
        while( a< MoveList.Count)
        {
            if (Vector2.Distance(unit.transform.position, map[MoveList[a].x, MoveList[a].y, MoveList[a].layer].transform.position) > 1)
            {
                unit.transform.position = map[MoveList[a].x, MoveList[a].y, MoveList[a].layer].transform.position;
            }
            else
            {
                while (unit.transform.position != map[MoveList[a].x, MoveList[a].y, MoveList[a].layer].transform.position)
                {
                    unit.transform.position = Vector3.MoveTowards(unit.transform.position, map[MoveList[a].x, MoveList[a].y, MoveList[a].layer].transform.position, unit.moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            unit.position = new int[] { MoveList[a].x, MoveList[a].y, MoveList[a].layer };
            unit.positionAsVector3 = new Vector3(MoveList[a].x, MoveList[a].y, MoveList[a].layer);
            a++;
        }
        //foreach (TileInMemory MoveList[a] in MoveList)
        //{
           
        //}
        //MoveList2 = MoveList;
        //IsMoving = false;

    }
}
