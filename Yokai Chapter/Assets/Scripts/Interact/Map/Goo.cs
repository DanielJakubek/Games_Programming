using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo : MonoBehaviour
{

    private float playerSpeed;
    public float speedDebuff = 0;

   
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Movement decrease");
        if(other.tag == "Player")
            EventManager.eventMngr.SpeedDebuff(speedDebuff);
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Movement Increase");
        if(other.tag == "Player")
            EventManager.eventMngr.SpeedIncrease();
    }

    private void OnDestroy() {
        EventManager.eventMngr.SpeedIncrease();
    }
}
