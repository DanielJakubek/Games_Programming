using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject doorPlatform;

    public WeaponTemplate pistol;
    public WeaponTemplate shotgun;

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
            pistol.ammo = 0;
            shotgun.ammo = 0;
        }
    }
}
