using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LeaveHalfPipe : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera halfpipeCam;
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().ChangeState(PlayerController.Instance.baseState);
        halfpipeCam.enabled = false;
    }
}
