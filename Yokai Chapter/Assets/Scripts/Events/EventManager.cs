using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager eventMngr; 


    //============ACTIONS============//
    public event Action<GameObject> closeCurrentMenu; //Event when item is clicked on the UI
    public event Action<GameObject> openNextMenu; //Event to open the next menu

    public event Action closeGame; //Event to open the next menu


    // Start is called before the first frame update
    private void Awake(){
        
        //Makes sure there is a single audio manager
        if(eventMngr == null){
            eventMngr = this;
        }else{
            Destroy(gameObject);
            return; 
        }

        DontDestroyOnLoad(gameObject); 
    }


    //Invokes the action if it is not null
    public void CloseMenuPanel(GameObject currentMenu){
        if(closeCurrentMenu != null)
            closeCurrentMenu(currentMenu);
    }

    //Invokes the action if it is not null
    public void OpenMenuPanel(GameObject nextMenu){
        if(openNextMenu != null)
            openNextMenu(nextMenu);
    }
    
    //Invokes the action if it is not null
    public void CloseGame(){
        if(closeGame != null)
            closeGame();
    }
}
