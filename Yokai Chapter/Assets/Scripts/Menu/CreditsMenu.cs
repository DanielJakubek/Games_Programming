using UnityEngine;

public class CreditsMenu : MenuParent
{
    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
            LoadScene("MainMenu");
    }
}
