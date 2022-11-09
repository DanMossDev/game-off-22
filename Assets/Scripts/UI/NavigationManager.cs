using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance {get; private set;}
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject pauseMenu;

    void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 

        HideMouse();
    }
    public void ShowMouse()
    {
        Time.timeScale = 0;
        //playerInput.DeactivateInput();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void HideMouse() 
    {
        Time.timeScale = 1;
        //playerInput.ActivateInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
