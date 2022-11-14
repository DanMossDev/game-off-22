using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] GameObject timeRemainingText;
    [SerializeField] GameObject levelScoreText;
    [SerializeField] GameObject totalScoreText;
    [SerializeField] TextMeshProUGUI timeRemaining;
    [SerializeField] TextMeshProUGUI levelScore;
    [SerializeField] TextMeshProUGUI totalScore;
    [SerializeField] GameObject gradeObject;
    [SerializeField] Image grade;
    [SerializeField] GameObject levelUI;

    int timeRemainingValue;
    int levelScoreValue;
    int totalScoreValue;
    void OnEnable()
    {
        levelUI.SetActive(false);
        timeRemainingValue = Mathf.RoundToInt(LevelManager.Instance.timeLeft);
        levelScoreValue = LevelManager.Instance.Score;
        totalScoreValue = (timeRemainingValue * 100) + levelScoreValue;

        StartCoroutine(RevealTime());

    }

    IEnumerator RevealTime()
    {
        yield return new WaitForSeconds(0.25f);
        timeRemainingText.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        timeRemaining.gameObject.SetActive(true);
        timeRemaining.text = $"{timeRemainingValue}";


        yield return new WaitForSeconds(0.25f);
        levelScoreText.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        levelScore.gameObject.SetActive(true);
        levelScore.text = $"{levelScoreValue}";


        yield return new WaitForSeconds(0.25f);
        totalScoreText.SetActive(true);
        totalScore.gameObject.SetActive(true);
        while (int.Parse(totalScore.text) < totalScoreValue)
        {
            if (int.Parse(timeRemaining.text) > 0)
            {
                timeRemaining.text = $"{int.Parse(timeRemaining.text) - 1}";
                totalScore.text = $"{int.Parse(totalScore.text) + 100}";
            }
            if (int.Parse(levelScore.text) > 0)
            {
                levelScore.text = $"{int.Parse(levelScore.text) - 100}";
                totalScore.text = $"{int.Parse(totalScore.text) + 100}";
            }
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(6);
        LevelManager.Instance.LoadNextLevel();
    }
}
