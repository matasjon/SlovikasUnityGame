using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomSpawmer : MonoBehaviour
{
    public int openingDirection;
    //1 bottom
    //2 top
    //3 left
    //4 right

    private RoomTemplates templates;

    private int rand;
    public bool original = false;
    private bool spawned = false;
    private manager manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<manager>();
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 1f);

    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //need to spawn a room with a bottom door
                rand = IndexGenerator(manager.roomCount, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                manager.roomCount++;
                CreateFloor();
            }
            else if (openingDirection == 2)
            {
                //need to spawn a room with a top door
                rand = IndexGenerator(manager.roomCount, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                manager.roomCount++;
                CreateFloor();
            }
            else if (openingDirection == 3)
            {
                //need to spawn a room with a left door
                rand = IndexGenerator(manager.roomCount, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                manager.roomCount++;
                CreateFloor();
            }
            else if (openingDirection == 4)
            {
                //need to spawn a room with a right door
                rand = IndexGenerator(manager.roomCount, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                manager.roomCount++;
                CreateFloor();
            }
            spawned = true;
        }
    }

    void CreateFloor()
    {
        int rnd = Random.Range(0, templates.floorTemplates.Length -1);
        Instantiate(templates.floorTemplates[rnd], transform.position, templates.floorTemplates[rnd].transform.rotation);
    }

    int IndexGenerator(int generated, int length)
    {
        int index = (int)(length / System.Math.Pow(2, generated / 10));
        // Debug.Log(index);
        if (index < 3) index = 3;
        int rand = Random.Range(0, index);
        return rand;
         
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint"))
        {
            spawned = true;
        }
        if (other.CompareTag("SpawnPoint"))
        {
            if (original == true)
            {
                original = false;
            }
            else
            {
                Destroy(gameObject);
                other.gameObject.GetComponent<RoomSpawmer>().original = true;
            }
        }
    }
}
