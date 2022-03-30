using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start(){
        EventManager.eventMngr.closeCurrentMenu += MenuClose;    
        EventManager.eventMngr.openNextMenu += MenuOpen;  
    }


    //Disables the ui object passed through
    public void MenuClose(){
        //Disable current menu panel
        Debug.Log("Working");
    }

    //Opens the ui object passed through
    public void MenuOpen(){
         //Enable new menu panel
        Debug.Log("Working");
    }
}
