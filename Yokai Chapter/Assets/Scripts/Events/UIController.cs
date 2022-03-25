using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{
    public TextMeshProUGUI TextPro;


     

    //Called once
    private void Start() {

        //Subscribing events
        

        EventManager.eventMngr.onPlayerDamage += UpdateHealthBar;
    }

    //Used to update the player's health bar
    private void UpdateHealthBar(){

        TextPro.text = Player.playerInstance.health.ToString();

       // TextPro.text = "Some String";
        //Debug.Log("Hp updated bruh");
        //EventManager.eventMngr.onPlayerDamage -= UpdateHealthBar;

        Player.playerInstance.health = 1000f;
    }

}
