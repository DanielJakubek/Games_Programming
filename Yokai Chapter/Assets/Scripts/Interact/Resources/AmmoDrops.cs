using UnityEngine;

/*
    Used to call call a check for a trigger.
    If the player enters the trigger and
    are not max armour they are healed to max armour
*/
public class AmmoDrops : Resource
{   
    //What weapon to increase
    public WeaponTemplate weaponTemplate;

    /*
        Checks if player hit the trigger, if so, 
    */
    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Player"){
            weaponTemplate.ammo += weaponTemplate.ammoDropCount;
            EventManager.eventMngr.OnAmmoUse(weaponTemplate.ammo);
            UpdateResource();
        }   
    }


}
