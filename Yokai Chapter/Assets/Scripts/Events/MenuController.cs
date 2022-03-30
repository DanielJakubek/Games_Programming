using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //Game objects
    public GameObject currentMenu,nextMenu;


    // Start is called before the first frame update
    private void Start(){
        EventManager.eventMngr.closeCurrentMenu += MenuClose;    
        EventManager.eventMngr.openNextMenu += MenuOpen;  
        EventManager.eventMngr.closeGame += CloseTheGame;  
    }


    //Disables the ui object passed through
    public void MenuClose(GameObject currentMenu){
        if(currentMenu == this.currentMenu)
            currentMenu.SetActive(false);     
    }

    //Opens the ui object passed through
    public void MenuOpen(GameObject nextMenu){
        if(nextMenu == this.nextMenu)
            nextMenu.SetActive(true);    
    }

    //Closes the game
    public void CloseTheGame(){
        Application.Quit();
    }

    //Unsubscribes the events from list upon their destruction
    private void OnDestroy() {
        EventManager.eventMngr.closeCurrentMenu -= MenuClose;    
        EventManager.eventMngr.openNextMenu -= MenuOpen;
        EventManager.eventMngr.closeGame -= CloseTheGame;  
    }
}
