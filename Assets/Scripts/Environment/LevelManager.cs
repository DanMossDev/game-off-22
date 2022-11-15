using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Amount of seconds to complete the level before gameover occurs")]
    public float timeLimit = 120;
    [Tooltip("The cinemachine camera which will become active to show the victory animation")][SerializeField]
    CinemachineVirtualCamera victoryCam;
    [Tooltip("The UI which is shown on victory")][SerializeField]
    GameObject victoryScreen;
    [Tooltip("The loading screen")][SerializeField]
    GameObject loadingScreen;
    [HideInInspector] public float timeLeft;
    [HideInInspector] public int Score = 0;


    [Space][Header("Level Grade Values")]
    public int Escore = 2000;
    public int Dscore = 3000;
    public int Cscore = 4000;
    public int Bscore = 5000;
    public int Ascore = 6000;
    public int Sscore = 7000;

    Coroutine Timer;

    [SerializeField] bool forceVictory = false;


    public static LevelManager Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Score = 0;
        timeLeft = timeLimit;
    }

    void Start()
    {
        Timer = StartCoroutine(CountDown());
    }

    void Update()
    {
        if (forceVictory) LevelComplete(); 
    }


    public void GameOver(GameOvers cause)
    {
        Menu.isPaused = true;
        Invoke("ReloadLevel", 2);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelComplete()
    {
        PlayerController.Instance.isVictorious = true;
        PlayerController.Instance.animator.SetTrigger("Victory");
        victoryCam.enabled = true;
        StartCoroutine(DisplayVictoryScreen());
        switch (Score)
        {
            case int n when n >= Sscore:
                print("S grade");
                break;
            case int n when n >= Ascore:
                print("A grade");
                break;
            case int n when n >= Bscore:
                print("B grade");
                break;
            case int n when n >= Cscore:
                print("C grade");
                break;
            case int n when n >= Dscore:
                print("D grade");
                break;
            case int n when n >= Escore:
                print("E grade");
                break;
            default:
                print("F grade");
                break;
        }
    }

    public void StopTimer()
    {
        StopCoroutine(Timer);
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

    IEnumerator DisplayVictoryScreen()
    {
        while (!PlayerController.Instance.isGrounded) yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(2);
        victoryScreen.SetActive(true);
    }

}


public enum GameOvers {
    Death,
    TimeOut
}