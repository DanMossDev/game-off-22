using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    int sceneIndex;
    [HideInInspector] public Vector3 playerLoadPoint;
    [HideInInspector] public int score;
    [HideInInspector] public int timeSpent;


    public static CheckpointManager Instance {get; private set;}
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(Instance);
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            playerLoadPoint = player.transform.position;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneIndex != scene.buildIndex)
        {
            Destroy(Instance);
        }
        else
        {
            PlayerController.Instance.transform.position = playerLoadPoint;
            LevelManager.Instance.Score = score;
            LevelManager.Instance.timeLeft -= timeSpent;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
