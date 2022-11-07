using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{ 
    new void OnDisable()
    {
        isPaused = false;
        main.SetActive(true);
        options.SetActive(false);
        if (NavigationManager.Instance) NavigationManager.Instance.HideMouse();
    }
    public void Resume()
    {
        gameObject.SetActive(false);
    }
}
