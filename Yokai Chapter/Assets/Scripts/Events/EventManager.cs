using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager eventMngr; 


    //============ACTIONS============//
    public event Action closeCurrentMenu; //Event when item is clicked on the UI
    public event Action openNextMenu; //Event to open the next menu


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
    public void CloseMenuPanel(){
        closeCurrentMenu?.Invoke();
    }

    //Invokes the action if it is not null
    public void OpenMenuPanel(){
        openNextMenu?.Invoke();
    }
}
