using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public GameObject optionsFirstButton, optionsClosedButton;
    public static bool isPaused;
    
    public void OnEnable() 
    {
        if (NavigationManager.Instance) NavigationManager.Instance.ShowMouse();
        isPaused = true;
    }

    public void OnDisable() 
    {
        isPaused = false;
        if (NavigationManager.Instance) NavigationManager.Instance.HideMouse();
    }

    public void Options()
    {
        main.SetActive(!main.activeSelf);
        options.SetActive(!options.activeSelf);

        if (options.activeSelf) {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsClosedButton);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
