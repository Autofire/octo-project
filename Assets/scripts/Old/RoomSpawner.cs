using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int RoomDir;
    //1 = up
    //2 = down
    //3 = left
    //4 = right

    private RoomTemplates templates;
    private int Rand;
    private bool spawned;

    void Start()
    {
        
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        
        Invoke("spawn", 0.1f);
        Debug.Log("works");
    }



    void spawn()
    {
        if(spawned == false)
        {
            if (RoomDir == 2)
            {
                Rand = Random.Range(0, templates.TopRooms.Length);
                Instantiate(templates.TopRooms[Rand], transform.position, Quaternion.identity);
            }
            else if (RoomDir == 1)
            {
                Rand = Random.Range(0, templates.BottomRooms.Length);
                Instantiate(templates.BottomRooms[Rand], transform.position, Quaternion.identity);
            }
            else if (RoomDir == 4)
            {
                Rand = Random.Range(0, templates.LeftRooms.Length);
                Instantiate(templates.LeftRooms[Rand], transform.position, Quaternion.identity);
            }
            else if (RoomDir == 3)
            {
                Rand = Random.Range(0, templates.RightRooms.Length);
                Instantiate(templates.RightRooms[Rand], transform.position, Quaternion.identity);
            }
            spawned = true;
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Spawnpoint") && other.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
