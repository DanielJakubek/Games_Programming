using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject doorPlatform;

    // Start is called before the first frame update
    void Start(){
        EventManager.eventMngr.firstBossDeath += FirstBossDeath;
    }



    ///<summary>
    ///Deals with what happens when the first boss dies in level one
    ///</summary>
    public void FirstBossDeath(){
        Pillars.pillarInstace.GetNewPillarPosition(-20f);
    
        if(doorPlatform != null)
            doorPlatform.SetActive(true);
    }













    //Called once when the gameobject is destroyed
    private void OnDestroy() {
        EventManager.eventMngr.firstBossDeath -= FirstBossDeath;
    }
}
