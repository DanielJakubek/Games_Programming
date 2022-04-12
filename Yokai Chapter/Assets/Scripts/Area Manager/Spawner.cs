using System.Collections.Generic;
using UnityEngine;

/*
    This class deals with spawning enemies upon request,
    It will have an area that can be set to whatever size, and 
    enemies can only spawn in that area.
*/
public class Spawner : MonoBehaviour
{
    public List<WaveTemplate> enemyWaves = new List<WaveTemplate>(); //List of information for each wave
    public string areaName; //Name of the current area
    public Vector2 spawnSize; //Size of the spawn area
    public LayerMask spawnable; //What surface the enemies can spawn on
    public LayerMask notSpawnable;

    private void Start() {
        EventManager.eventMngr.startWave += SpawnWave; //Subs function to event
    }

    /*
        Used to spawn the correct enemy in the current scenario.
        This is done by making sure that the correct area was chosen and
        then checking if there are scriptable objects for the waves, if so
        will loop to spawn enemies for that wave
    */
    private void SpawnWave(string areaName, int wave){
        if(this.areaName == areaName){
            if(enemyWaves.Count > wave){
                foreach(var enemies in enemyWaves[wave].enemyList){
                    SpawnEnemy(enemies.enemyName, enemies.amountOfEnemies);
                }
            }
        }
    }

    /*
        Used in order to spawn the requested enemy. Does this by first checking if
        the enemy exists, if so, it loops for  the requested number of enemies, getting
        a random world position and instantiating an enemy entity there.

        Paramters:
        enemyName, a string that holds the information as to what enemy to spawn.
        numberOfEnemies, an int which tells it how many enemies to spawn
    */
    public void SpawnEnemy(string enemyName, int numberOfEnemies){

        //Requests the enemy from the prototype
        GameObject enemyToSpawn = Prototype.prototypeInstance.GetEnemy(enemyName);

        /*
            For the number of enemies requested, check if there is an enemy to spawn, if
            there is then, get a random spawn location, it it's not 0,0,0 (arbitrary position)
            instantiate the enemy at that location
        */
        if(enemyToSpawn != null){
            for(int i = 0; i < numberOfEnemies; i++){
                
                Vector3 spawnPosition = FindSpawnLocation();

                if(spawnPosition != Vector3.zero){
                    Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

                    int temp = EnemyCounter.enmyCounterInstace.getAmountOfEnemies() +1;
                    EnemyCounter.enmyCounterInstace.setAmountOfEnemies(temp);
                }
            }
        }
    }

    /*
        Finds a random location in the grid to spawn the enemy. This is done by
        generating two random numbers between 0 and the vector x and y. Check sphere to see
        if the layer mask allows for spawning, if so, exit while loop and return spawn location information.
        ~~ This can be improved for better performance if you create a grid instead of using a scuffed do while loop ~~

        Returns: vector 3, spawnPosition, the position that will be used to spawn in the enemy. If could not find
        location then returns null
    */
    private Vector3 FindSpawnLocation(){
        
        int counter = 0;

        //Gets the bottom left of the grid
        Vector3 gridBottomLeft = transform.position - Vector3.right * spawnSize.x/2 - Vector3.forward * spawnSize.y/2;

        //If the current location allows for spawning
        bool canSpawn = false;

        do{
            //Finds random location
            float randomX = Random.Range(0,spawnSize.x);
            float randomY = Random.Range(0,spawnSize.y);

            //Transforms the grid location to the real game world position
            Vector3 realPosition = gridBottomLeft + Vector3.right * (randomX) + Vector3.forward * (randomY);

            if(Physics.CheckSphere(realPosition, 2f, spawnable)){
                if(!Physics.CheckSphere(realPosition, 2f, notSpawnable)){
                    canSpawn = true;
                    return realPosition;
                }
                
            }else 
                counter++;

        //Loops while a sufficent spawn location has not been found or 5 attempts have been made
        }while(!canSpawn || counter < 5);

        return Vector3.zero;
    }

  
    //Used to see how large the spawn area is
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnSize.x, 0, spawnSize.y));
    }

    //Unsubs the event when destroyed
    private void OnDestroy() {
         EventManager.eventMngr.startWave -= SpawnWave;
    }
}
