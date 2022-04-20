using UnityEngine;

/*
    Used to call call a check for a trigger.
    If the player enters the trigger they will
    pick up the weapon
*/
public class WeaponDrop : Resource
{   
    public GameObject weapon; //Weapon to be picked up
    public GameObject weaponManager; //Where the weapon will go

    [Header("Weapon tranform")]
    public Vector3 weaponLocation;
    public Vector3 weaponRotation;
    public Vector3 weaponScale;
    
    /*
        Checks if there is a wepaon to pick up and where it goes.
        If there are, then 
    */
    private void OnTriggerEnter(Collider other) {

        if(weaponManager !=null && weapon != null){

            //Creates the object in the scene and makes it a child if the weapon manager
            GameObject temp = Instantiate(weapon, weaponLocation, Quaternion.Euler(0f, 0f, 0f));
            temp.transform.parent = weaponManager.transform;
            
            //Sets the weapon's transform to look good in the game
            temp.transform.localPosition = weaponLocation;
            temp.transform.localEulerAngles = weaponRotation;
            temp.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y, weaponScale.z);

            UpdateResource();
        }  
    }
}
