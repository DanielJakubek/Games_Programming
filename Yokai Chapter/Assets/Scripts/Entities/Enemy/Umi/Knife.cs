using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{   

    private void OnTriggerEnter(Collider collider) {

        

        if(collider.transform.tag == "Player"){
            Player.playerInstance.UpdateHealth(10f);
        }
    }

}
