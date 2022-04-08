using UnityEngine;

/*
    Class ued to keep count of how many enmies there are
    in the level, used when creating enemies and destroying enemies
*/
public class EnemyCounter : MonoBehaviour{

    private int amountOfEnemies = 0; //Current number of enemies
    public static EnemyCounter enmyCounterInstace;  //Instance of this object

    // Start is called before the first frame update
    void Awake(){

        //Makes sure there is a single audio manager
        if(enmyCounterInstace == null){
            enmyCounterInstace = this;
        }else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //Setter
    public void setAmountOfEnemies(int amountOfEnemies){
        this.amountOfEnemies = amountOfEnemies;
    }

    //Getter
    public int getAmountOfEnemies(){
        return amountOfEnemies;
    }
}
