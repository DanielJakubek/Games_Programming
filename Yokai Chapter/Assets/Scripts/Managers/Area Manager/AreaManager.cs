using UnityEngine;
using System;

/*
    Purpose of this class is to spawn an enemy
    wave when the trigger area has been entered. After
    trigger area has been activated it will spawn enemies after
    all enemies die per wave
*/
public class AreaManager : MonoBehaviour
{
    public int numberOfWaves,currentWave; //Number of waves and tracker of currnet wave
    public string areaName; //What area the spawner is responsilbe for
    public int spawnNextAfter = 0; //How many enemies remaning before next wave should spawn
    private bool triggered,triggerOnce = false;  //The trigger area

    public bool closeDoors = true;
    
    // Update is called once per frame
    void Update()
    {   
        //If the player has entered the trigger area and there are more waves to go
        if(triggered){
            if(EnemyCounter.enmyCounterInstace.getAmountOfEnemies() <= spawnNextAfter){
                
                //Activate the action to start the wave, parsing the name of this area and what wave
                EventManager.eventMngr.StartWave(areaName, currentWave);
                currentWave++;

                //When last wave, make trigged false to no longer update
                if(numberOfWaves < currentWave){
                    triggered = false;

                    if(closeDoors){
                        try{
                            EventManager.eventMngr.CloseAllDoors();
                        }catch(Exception e){ Debug.Log(e); }
                    }
                }
            }
        }
    }

    /*
        Makes sure that the trigger is activate once and 
        by the player. Also closes all doors and makes then
        unopenable
    */
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" || other.tag == "Trigger"){
            if(!triggerOnce){

                if(closeDoors){
                    try{
                        EventManager.eventMngr.CloseAllDoors();
                    }catch(Exception e){ Debug.Log(e); }
                }

                triggerOnce = true;
                triggered = true;
            }
        }
    }
}
