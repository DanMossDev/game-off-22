using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().ChangeState(PlayerController.Instance.baseState);
        LevelManager.Instance.LevelComplete();
        gameObject.SetActive(false);
    }
}
