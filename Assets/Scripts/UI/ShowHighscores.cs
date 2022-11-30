using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHighscores : MonoBehaviour
{
    [SerializeField] GameObject highscores;
    [SerializeField] GameObject submission;
    [SerializeField] GameObject main;

    [SerializeField] TextMeshProUGUI errorMessage;

    public static ShowHighscores Instance {get; private set;}

    void Awake()
    {
        Instance = this;
    }

    public void ToggleHighscores()
    {
        errorMessage.text = "";
        main.SetActive(!main.activeSelf);
        highscores.SetActive(!highscores.activeSelf);
    }

    public void ToggleSubmit()
    {
        errorMessage.text = "";
        highscores.SetActive(!highscores.activeSelf);
        submission.SetActive(!submission.activeSelf);
    }
}
