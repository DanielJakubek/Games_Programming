using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{




    // Update is called once per frame
    void Update()
    {
        
        if(EnemyCounter.enmyCounterInstace.getAmountOfEnemies() <= 0){
            //Next wave

            //Event telling the spawner to go to the next wave

        }

    }
}
