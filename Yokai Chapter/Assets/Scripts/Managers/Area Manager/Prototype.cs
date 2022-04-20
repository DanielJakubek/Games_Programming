using UnityEngine;

/*
    The prototype game pattern. This keeps a list of
    all the enemies and is responsible for the spawner
    being able to pick an enemy gameobject at will.
*/
public class Prototype : MonoBehaviour
{   
    public static Prototype prototypeInstance; //Instance of this object
    public GameObject[] enemies; //List of enemies in the game
    public GameObject[] items; //List of items in the game

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

    /*
        Getter for the enemy game object.
        Iterates through the list of enemies until it finds
        the one that is being looked for.

        Paramters: enemyName, the name of the gameobject to look for
        Returns: gameobject, the enemy to be cloned. Null if nothing found
    */
    public GameObject GetItem(string itemName){

        foreach(GameObject item in items){
            if(item.name == itemName)
                return item;
        }
        return null;
    }
}
