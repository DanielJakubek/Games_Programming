using UnityEngine;
using TMPro;

/*
    Deals with executing HUD based actions.
    This mainly deals with changing the values on the HUD
    such as player Health, ammo and armour.
*/
public class HUDController : MonoBehaviour
{   
    public TextMeshProUGUI healthText; // The text component for the health
    public TextMeshProUGUI armourText; // The text component for the armour
    
    // Start is called before the first frame update
    private void Start(){
        EventManager.eventMngr.updatePlayerHealth += UpdateHudHealth;
        EventManager.eventMngr.updatePlayerArmour +=UpdateHudArmour;
    }

    /*
        Updates the player health on the HUD.
        Takes in the paramter playerHealth which is
        the health of the player
    */
    public void UpdateHudHealth(float playerHealth){
        healthText.text = playerHealth.ToString() +"%";
    }

    /*
        Updates the player armour on the HUD.
        Takes in the paramter playerArmour which is
        the armour of the player
    */
    public void UpdateHudArmour(float playerArmour){
        armourText.text = playerArmour.ToString() +"%";
    }


    //Unsubscribes the events from list upon their destruction
    private void OnDestroy() {
        EventManager.eventMngr.updatePlayerHealth -= UpdateHudHealth;
        EventManager.eventMngr.updatePlayerArmour -=UpdateHudArmour;
    }
}
