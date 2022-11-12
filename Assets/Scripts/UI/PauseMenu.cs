using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : Menu
{ 
    public GameObject pauseFirstButton;

    new void OnEnable()
    {
        if (NavigationManager.Instance) NavigationManager.Instance.ShowMouse();
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }
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
