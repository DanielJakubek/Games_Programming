using UnityEngine;

/*
    Purpose of this class is to spawn an enemy
    wave when the trigger area has been entered. After
    trigger area has been activated it will spawn enemies after
    all enemies die per wave
*/
public class AreaManager : MonoBehaviour
{
    //Number of waves and tracker of currnet wave
    public int numberOfWaves,currentWave;
    public string areaName; //What area the spawner is responsilbe for

    //The trigger area
    private bool triggered,triggerOnce = false;
   
    // Update is called once per frame
    void Update()
    {   
        //If the player has entered the trigger area and there are more waves to go
        if(triggered){
            if(EnemyCounter.enmyCounterInstace.getAmountOfEnemies() <= 0){
                
                //Activate the action to start the wave, parsing the name of this area and what wave
                EventManager.eventMngr.StartWave(areaName, currentWave);
                currentWave++;

                //When last wave, make trigged false to no longer update
                if(numberOfWaves < currentWave)
                    triggered = false;
            }
        }
    }

    /*
        Makes sure that the trigger is activate once and 
        by the player
    */
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            if(!triggerOnce){
                triggerOnce = true;
                triggered = true;
            }
        }
    }
}
