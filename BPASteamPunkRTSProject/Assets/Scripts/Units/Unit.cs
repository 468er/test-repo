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
        //If there are tiles to move to
        if(moveTiles.Count > 0) 
        {
            //idk why I did a second check
            if (0 < moveTiles.Count)
            {
                //Tunnels work by merely ordering the Unit to go to the exit tile. All tiles are 1 meter away from each other. Thus, checking if the distance
                //is equal or greater to 2, and then simply teleporting the unit to the tile if that's true, creates an instance which is only used for tunnel functionality.
                int a = 0;
                Tile CurrentTile = map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].GetComponent<Tile>();
                if (Vector2.Distance(transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position) >= 2)
                {
                    transform.position = map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position;
                }
                else
                {
                    //if current position != the position of the next tile to move to
                    if (transform.position != map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position, moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        //if the current position = the next tile to move to (also the tile currently on, now).

                        CurrentTile.Ability(this);
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
