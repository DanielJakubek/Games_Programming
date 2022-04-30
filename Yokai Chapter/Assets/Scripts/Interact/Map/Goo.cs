using UnityEngine;

///<summary>
///This class is used for the object which deals with decreasing the speed of the player
///</summary>
public class Goo : MonoBehaviour
{
    public float speedDebuff = 0; //How the speed will be effect

   //when the player enters the trigger call the speed debuff event
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
            EventManager.eventMngr.SpeedDebuff(speedDebuff);
    }   

    //when the player exits the trigger call the speed increase event
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
            EventManager.eventMngr.SpeedIncrease();
    }

    //Increase speed on destroy
    private void OnDestroy() {
        EventManager.eventMngr.SpeedIncrease();
    }
}
