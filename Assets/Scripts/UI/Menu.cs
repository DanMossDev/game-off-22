using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
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
    }

    public void Quit()
    {
        Application.Quit();
    }
}
