using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Score;
    [SerializeField] TextMeshProUGUI Time;
    [SerializeField] TextMeshProUGUI HP;

    void Update()
    {
        Score.text = $"Score: {LevelManager.Instance.Score}";
        Time.text = $"{LevelManager.Instance.timeLeft}";
        HP.text = $"HP: {PlayerController.Instance.hitPoints.currentHP}";
    }
}
