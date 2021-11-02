using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour {

    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT

    private int direction;
    private bool stopGeneration;
    private int downCounter;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public LayerMask whatIsRoom;

    public GameObject player;
    

    private void Start()
    {
       
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[1], transform.position, Quaternion.identity);
        player.transform.position = transform.position + new Vector3(0, 5, 0);
        StartCoroutine("playerFall");


        direction = Random.Range(1, 6);
    }

    private void Update()
    { 
        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    private void Move()
    {

        if (direction == 1 || direction == 2)
        { // Move right !
          
            if (transform.position.x < 25)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                // Makes sure the level generator doesn't move left !
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move left !
           
            if (transform.position.x > 0)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else {
                direction = 5;
            }
           
        }
        else if (direction == 5)
        { 
            downCounter++;
            if (transform.position.y > -24.5)
            {

                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
                Debug.Log(previousRoom);
                if (previousRoom.GetComponent<Room>().roomType != 4 && previousRoom.GetComponent<Room>().roomType != 2)
                { 
                    if (downCounter >= 2)
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        Instantiate(rooms[4], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(2, 5);
                        if (randRoomDownOpening == 3)
                        {
                            randRoomDownOpening = 2;
                        }
                        Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                    }

                }
                
               
  
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                // Makes sure the room we drop into has a TOP opening !
                int randRoom = Random.Range(3, 5);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else {
                stopGeneration = true;
            }
            
        }
    }

    IEnumerator playerFall()
    {
        //Make invunrable
        Physics2D.IgnoreLayerCollision(9, 11, true);
        
        yield return new WaitForSeconds(0.3f);
        //Make vaunrable 
        Physics2D.IgnoreLayerCollision(9, 11, false);
        
    }
}
