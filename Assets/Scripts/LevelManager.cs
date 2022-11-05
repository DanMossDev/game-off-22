using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Amount of seconds to complete the level before gameover occurs")][SerializeField]
    float timeLimit = 120;
    public float timeLeft;
    [HideInInspector] public int Score = 0;


    [Space][Header("Level Grade Values")]
    public int Escore = 2000;
    public int Dscore = 3000;
    public int Cscore = 4000;
    public int Bscore = 5000;
    public int Ascore = 6000;
    public int Sscore = 7000;


    public static LevelManager Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        Score = 0;
        timeLeft = timeLimit;
        StartCoroutine(CountDown());
    }


    public void GameOver(GameOvers cause)
    {
        Invoke("ReloadLevel", 3);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelComplete()
    {
        Score += Mathf.RoundToInt(timeLeft) * 100;
        switch (Score)
        {
            case int n when n >= Sscore:
                //S grade;
                break;
            case int n when n >= Ascore:
                //A grade;
                break;
            case int n when n >= Bscore:
                //B grade;
                break;
            case int n when n >= Cscore:
                //C grade;
                break;
            case int n when n >= Dscore:
                //D grade;
                break;
            case int n when n >= Escore:
                //E grade;
                break;
            default:
                //F grade
                break;
        }
    }

    IEnumerator CountDown()
    {
        while (timeLeft >= 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

        GameOver(GameOvers.TimeOut);
    }

}


public enum GameOvers {
    Death,
    TimeOut
}