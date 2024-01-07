using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unit : MonoBehaviour
{
    public UnitType Indentifier;
    public string nameAsString;
    public float IntervalBetweenFiring;
    public float moveSpeed = 5f;
    public float Health = 10;
    public float MaxHealth = 10;
    public float Damage = 10;
    public float Range = 10;
    public Ability Ability;

    
    public int[] position = new int[] { 0, 0, 0 };
    public Vector3 positionAsVector3;
    public List<TileInMemory> moveTiles = new List<TileInMemory>();
    public bool moving = false;
    public TileInMemory lastTile;

    public Coroutine movingRoutine;
    public GameObject transporter = null;

    
    public bool isEnemy = false;
    public PlayerController user;
    public List<GameObject> targets = new List<GameObject>();
    public bool StopAfterTargetDeaths;
    public GameObject[,,] map;
    // Start is called before the first frame update
    public void Start()
    {
        positionAsVector3 = new Vector3(position[0], position[1], position[2]);
    }
    // Update is called once per frame
    public void Update()
    {
        if(moveTiles.Count > 0) 
        {
            if (0 < moveTiles.Count)
            {

                int a = 0;
                if (Vector2.Distance(transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position) >= 2)
                {
                    transform.position = map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position;
                }
                else
                {
                    if (transform.position != map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position, moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        position = new int[] { moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer };
                        positionAsVector3 = new Vector3(moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer);
                        lastTile = moveTiles[a];
                        moveTiles.Remove(moveTiles[a]);
                    }
                }
            }
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    targets.Remove(targets[i]);
                    i--;
                }
            }
            foreach (GameObject obj in targets)
            {
                Attack(obj, true);
            }
        }
        else
        {
            moving = false;
        }
    }
    public void MoveOverTime(List<TileInMemory> MoveList, GameObject[,,] Map, Unit unit)
    {
        moving = true;
        int a = 0;
        while (a < MoveList.Count)
        {
            moveTiles.Add(MoveList[a]);
            a++;
        }
        a = 0;
        if(targets.Count > 0) {
               
            }
        map = Map;
        //while( 0 < moveTiles.Count)
        //{
            
           
        //        if (Vector2.Distance(unit.transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position) > 1)
        //        {
        //            unit.transform.position = map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position;
        //        }
        //        else
        //        {
        //            while (unit.transform.position != map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position)
        //            {
        //                unit.transform.position = Vector3.MoveTowards(unit.transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position, unit.moveSpeed * Time.deltaTime);
        //                yield return null;
        //            }
        //        }
        //        unit.position = new int[] { moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer };
        //        unit.positionAsVector3 = new Vector3(moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer);
        //        lastTile = moveTiles[a];
        //        moveTiles.Remove(moveTiles[a]);
            
        //}
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
    public void Attack(GameObject receiver, bool moving)
    {
        if (Vector2.Distance(this.transform.position, receiver.transform.position) <= Range)
        {
            receiver.GetComponent<Unit>().TakeDamage(this.gameObject);
        }
        else
        {
            if(moving == false)
            {
                List<GameObject> list = new List<GameObject>();
                list.Add(receiver);
                List<GameObject> list2 = new List<GameObject>();
                list2.Add(this.gameObject);
                StopAfterTargetDeaths = true;
                targets.Add(receiver);
                user.OrderUnits(receiver.transform.position, list, list2);
            }
        }
    }
    public void TakeDamage(GameObject attacker)
    {
        if (Health - attacker.GetComponent<Unit>().Damage > 0)
        {
            Health -= attacker.GetComponent<Unit>().Damage;
        }
        else
        {
            Die();
        }
      
    }
    public void Die()
    {
        Debug.Log("Enemy has been killed");
        moveTiles.Clear();
        targets.Clear();
        GameObject.Destroy(this.gameObject);
    }
}
public enum Ability
{
    soldier,
    
}
