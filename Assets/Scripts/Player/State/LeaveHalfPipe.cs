using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHalfPipe : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().ChangeState(PlayerController.Instance.baseState);
    }
}
