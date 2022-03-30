using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
    Shows an error with UI not existing in the namespace, however, it seems
    to work fine and compile fine.
*/
public class MainMenu : MonoBehaviour
{

    public Button startBtn; //The start Button

    // Start is called before the first frame update
    void Start(){
        Button btn = startBtn.GetComponent<Button>();
        btn.onClick.AddListener(CloseAllMenus);
    }



    //Deals with activating an action
    private void CloseAllMenus(){
        EventManager.eventMngr.CloseMenuPanel();
    }

}
