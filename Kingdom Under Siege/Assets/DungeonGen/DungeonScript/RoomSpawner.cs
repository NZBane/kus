using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public int OpeningDirection;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 1f); //time delay of spawn
    }


    void Spawn()
    {
        if(spawned == false)
        {
            if (OpeningDirection == 1)
            {
                //spawn room with bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation); //can disable room rotation with Quaternion.Identity
            }
            else if (OpeningDirection == 2)
            {
                //spawn room with top door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation); //can disable room rotation with Quaternion.Identity
            }
            else if (OpeningDirection == 3)
            {
                //spawn room with left door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation); //can disable room rotation with Quaternion.Identity
            }
            else if (OpeningDirection == 4)
            {
                //spawn room with right door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation); //can disable room rotation with Quaternion.Identity
            }
            spawned = true;
        }
        


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint")){
            Destroy(gameObject);
        }

    }



}
