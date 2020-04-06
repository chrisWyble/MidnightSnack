using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawn : MonoBehaviour
{
    public GameObject foodPrefab;       // create prefab referances
    public float spawnDelayTm;                            // create spawn time variable
    public float appleDist, grapeDist;
    public bool moreFruit = false;

    // Start is called before the first frame update

    GameObject[] Leafs = new GameObject[100];           // 25 trees * 4 poosible spawn leafs
    GameObject[] Apples, Grapes = new GameObject[100];
    void Start()
    {
        Leafs = GameObject.FindGameObjectsWithTag("LEAF");  // the leafs where the fruit can spawned are tagged LEAF
        Invoke("fruitSpawn", 1.0f);  // spawn fruit after 1s
    }


    void Update()
    {

        Apples = GameObject.FindGameObjectsWithTag("APPLE");
        Grapes = GameObject.FindGameObjectsWithTag("GRAPES");
        
        for (int i = 0; i < 100; i++)
        {

            try
            {
                appleDist = Vector3.Distance(Apples[i].transform.position, Leafs[i].transform.position); // need to get the latest distance
                grapeDist = Vector3.Distance(Grapes[i].transform.position, Leafs[i].transform.position);
                print(appleDist);
                print(grapeDist);
            }
            catch (Exception e)     // catch the out of bounds error for the GameObj arrays
            {
                continue;
            }
            
            //TODO add math approimately or find better way to check if fruit was dropped

            if ((appleDist < 25.0f) || (grapeDist < 25.0f))   // Vector 3 of Apple and grapes to distance of LEAFS            
            {
                continue;
            }
            else
            {
                moreFruit = true; 
            } 

        }
        if (moreFruit)
        {
            Invoke("fruitSpawn", 5.0f);
            moreFruit = false;
        }

    }

    void OnCollisionEnter(Collision obj)    // detect collision
    {
        if (obj.gameObject.tag == "BAT")  //TODO give bat "BAT tag"
        {
            Destroy(obj.gameObject); // destroy Food when hit by bat
        }
                
    }

    void fruitSpawn()
    {
        GameObject meal;        // create a game obj variable holder
        Vector3 posScr, posWC;  // position for food and camera
        posScr = Input.mousePosition;   // mouse position
        posScr.y = this.transform.position.y-3.0f; // lock y-axis

        posWC = Camera.main.ScreenToWorldPoint(posScr); // initilizes position with camera position
        posWC.x = this.transform.position.x;    // lock new x and new z axises
        posWC.z = this.transform.position.z;


        meal = GameObject.Instantiate(foodPrefab, posWC, Quaternion.identity);  // create a new food clone

    }
}
