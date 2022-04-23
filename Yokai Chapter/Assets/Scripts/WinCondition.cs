using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        EventManager.eventMngr.LevelComplete();
    }
}
