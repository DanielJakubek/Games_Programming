using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject doorPlatform; //The platfrom that spawns from the final room
    public WeaponTemplate pistol; //The pistol weapon template
    public WeaponTemplate shotgun; //The shotgun weapon template

    // Start is called before the first frame update
    void Start(){

        SetAmmo();
        EventManager.eventMngr.firstBossDeath += FirstBossDeath;
        AudioManager.mngInstance.PlaySound("MusicStartArea", AudioManager.mngInstance.sounds);
    }

    ///<summary>
    ///Deals with what happens when the first boss dies in level one
    ///</summary>
    public void FirstBossDeath(){

        if(Pillars.pillarInstace !=null)
            Pillars.pillarInstace.GetNewPillarPosition(-20f);
    
        if(doorPlatform != null)
            doorPlatform.SetActive(true);
    }

    //Called once when the gameobject is destroyed
    private void OnDestroy() {
        EventManager.eventMngr.firstBossDeath -= FirstBossDeath;
    }

    ///<summary>
    ///Sets the ammo of all weapons in the level to 0 
    ///</summary>
    private void SetAmmo(){
        if(pistol != null && shotgun != null){
            pistol.ammo = 25;
            shotgun.ammo = 25;
        }
    }
}
