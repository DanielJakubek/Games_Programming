using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Deals with selecting a new weapon from the list
    of children that are attached to this game object

    Code inspired by:
    Barckeys
    https://youtu.be/Dn_BUIVdAPg
*/
public class WeaponManager : MonoBehaviour
{   
    private int currentWeapon = 0; //The currently selected weapon

    // Start is called before the first frame update
    private void Start(){
        EnableWeapon();
    }

    // Update is called once per frame
    private void Update(){
        SwitchWeapon();
    }

    /*
        Used to get the input from the player. Upon this input,
        it will be decided what weapon to switch to. This is done
        but looping around an "array" of children in the weapon
        manager parent object.
    */
    private void SwitchWeapon(){

        int prevWeapon = currentWeapon;

        //Scroll Forward
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){

            //Increase weapon counter if not at the end of list, otherwise go back to start.
            if(currentWeapon >= transform.childCount - 1)
                    currentWeapon = 0;
                else
                    currentWeapon++;
        }

        //Scroll Backward
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f){ 

            //Decrease weapon counter if not at the start of list, otherwise go back to end.
            if(currentWeapon <= 0)
                currentWeapon = transform.childCount - 1;
            else    
                currentWeapon--;
        }

        //If there has been a weapon switch
        if(prevWeapon !=currentWeapon)
             EnableWeapon();
    }
    
    /*
        Enables the gameobject is it has been selected
        otherwise, disables it.
    */
    private void EnableWeapon(){

        int temp = 0; // Temp value to keep track of weapon in list
        
        //For each child in the list, check if match has been found, if so, enable, otherwise, disable.
        foreach(Transform child in transform){
            if(currentWeapon == temp)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);

            temp++;
        }
    }
}
