using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        CheckpointManager.Instance.playerLoadPoint = transform.position;
        CheckpointManager.Instance.score = LevelManager.Instance.Score;
        CheckpointManager.Instance.timeSpent = Mathf.RoundToInt(LevelManager.Instance.timeLimit - LevelManager.Instance.timeLeft);
    }
}
