using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    public void Begin()
    {
        SceneManager.LoadScene(1);
    }
}
