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
    public Ability ability;

    
    public int[] position = new int[] { 0, 0, 0 };
    public float lastFired;
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
    public bool inRange = false;
    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    private void Awake()
    {
        //if (GetComponent<Rigidbody2D>() != null)
        //{
        //    rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        //    rigidbody2D.gravityScale = 0;
        //    rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        //    rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        //}
        //else
        //{
        //    rigidbody2D = gameObject.AddComponent<Rigidbody2D>();

        //    rigidbody2D.gravityScale = 0;
        //    rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        //    rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        //}
    }
    public void Start()
    {
        if(position == null)
        {
            position = MappingTools.WorldPositionToPositionArray(transform.position);
        }
        positionAsVector3 = new Vector3(position[0], position[1], position[2]);
        user = GameObject.Find("Player1").GetComponent<PlayerController>();
        lastFired = Time.time;
       
       
    }
    // Update is called once per frame
    public void Update()
    {
          
            if (0 < moveTiles.Count && inRange != true)
            {

                int a = 0;
                if (Vector2.Distance(transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position) >= 2)
                {
                    transform.position = map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position;
                }
                else
                {
                    Tile CurrentTile = map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].GetComponent<Tile>();
                    if (transform.position != map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, map[moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer].transform.position, moveSpeed * Time.deltaTime);
                        
                    //cound't figure out how to get rotation to work so I gave up.
                    //if(transform.rotation.eulerAngles.z != AngleBetween + 90)
                    //{

                    //}
                    //transform.LookAt(CurrentTile.transform.position);
                    //Vector3 vectorToTarget = CurrentTile.transform.position - transform.position;
                    //float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
                }
                    else
                    {
                        CurrentTile.Ability(this);
                        position = new int[] { moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer };
                        positionAsVector3 = new Vector3(moveTiles[a].x, moveTiles[a].y, moveTiles[a].layer);
                        lastTile = moveTiles[a];
                        moveTiles.Remove(moveTiles[a]);
                        if(moveTiles.Count == 0)
                        {
                        moving = false;
                        }
                    }
                }
            }
            if(moveTiles.Count == 0)
        {
            moving = false;
        }
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    targets.Remove(targets[i]);
                    i--;
                }
            }
        if (targets.Count > 0)
        {
            if (Vector2.Distance(this.transform.position, targets[0].transform.position) >= Range)
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }
        }
        else
        {
            inRange = false;
        }
        foreach (GameObject obj in targets)
            {

            UseAbility(obj, true);
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
        TileInMemory LastTile = lastTile;
        moveTiles.Clear();
        moving = false;
        return LastTile;
    }
    public void BecomeBuilding(GameObject buildingPrefab, Inventory playerInventory)
    {
        playerInventory.Remove(Resource_Type.Titanium.ToString(), 0);
        GameObject building = Instantiate(buildingPrefab, this.transform.position, Quaternion.identity);
        building.GetComponent<Pathing>().position = position;
        BuildingUnpackager building1 = building.GetComponent<BuildingUnpackager>();
        building1.Load();
        Destroy(this.gameObject);
    }
    public void UseAbility(GameObject receiver, bool idk)
    {
        switch (ability)
        {
            case Ability.soldier:
                AttemptedMurder(receiver, idk);
                break;
            case Ability.Worker:
                Mine(receiver, idk);
                break;
        }
    }
    public void AttemptedMurder(GameObject receiver, bool idk)
    {
        if (receiver.CompareTag("Unit"))
        {
            Attack(receiver, idk);
        }
       
    }     
    public void Attack(GameObject receiver, bool idk)
    {
        if (!targets.Contains(receiver))
        {
            targets.Add(receiver);
        }
        if (Vector2.Distance(this.transform.position, receiver.transform.position) <= Range)
        {
            inRange = true;
            if (lastFired + IntervalBetweenFiring <= Time.time)
            {
                lastFired = Time.time;
                switch (receiver.tag)
                {
                    case "Unit":
                        receiver.GetComponent<Unit>().TakeDamage(this.gameObject);

                        break;
                    case "ResourceDep":
                        receiver.GetComponent<ResourceDep>().TakeDamage(this.gameObject);
                        break;
                    case "Building":
                        receiver.GetComponent<Building>().TakeDamage(this.gameObject);
                        break;
                }
            }
        }
        else
        {
            inRange = false;
            if (moving == false)
            {
                List<GameObject> list = new List<GameObject>();
                list.Add(receiver);
                List<GameObject> list2 = new List<GameObject>();
                list2.Add(this.gameObject);
                StopAfterTargetDeaths = true;
               
                user.OrderUnits(receiver.transform.position, list, list2);
            }
        }
    }  
    public void Mine(GameObject receiver, bool idk)
    {
        if (receiver.CompareTag("ResourceDep"))
        {
            Attack(receiver, idk);
        }
       
    }
    public void TakeDamage(GameObject attacker)
    {
        if (Health - attacker.GetComponent<Unit>().Damage > 0)
        {
            Health -= attacker.GetComponent<Unit>().Damage;
            Debug.Log(attacker.gameObject + " <- Hit ->" + this.gameObject + "for " + attacker.GetComponent<Unit>().Damage + "reducing the health to " + Health);
        }
        else
        {
            attacker.GetComponent<Unit>().inRange = false;

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

