using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    [Tooltip("The loading screen")][SerializeField]
    GameObject loadingScreen;
    public void Begin()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }
}
