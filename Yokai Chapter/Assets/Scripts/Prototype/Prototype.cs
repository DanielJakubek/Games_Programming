using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype : MonoBehaviour
{   
    //Instance of this object
    public static Prototype prototypeInstance; 

    public GameObject[] enemies; //List of enemies in the game

    // Start is called before the first frame update
    private void Awake(){

        //Makes sure there is a single audio prototype
        if(prototypeInstance == null){
            prototypeInstance = this;
        }else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);  
    }

    /*
        Getter for the enemy game object.
        Iterates through the list of enemies until it finds
        the one that is being looked for.

        Paramters: enemyName, the name of the gameobject to look for
        Returns: gameobject, the enemy to be cloned. Null if nothing found
    */
    public GameObject GetEnemy(string enemyName){

        foreach(GameObject enemy in enemies){
            if(enemy.name == enemyName)
                return enemy;
        }
        return null;
    }
  
}
