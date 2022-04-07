using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    //How many enemies and what enemies

    // Start is called before the first frame update
    void Start(){

        SpawnEnemy("Okubi", 3);
        
    }


    /*
        We want to now...

        get the enemy we're looking for

    */




    private void SpawnEnemy(string enemyName, int numberOfEnemies){

        GameObject temp = Prototype.prototypeInstance.GetEnemy("Okubi");

        if(temp != null){


            //In a random location in a certain radius

            Instantiate(temp, transform.position+Vector3.up, Quaternion.identity);



        }


    }
}
