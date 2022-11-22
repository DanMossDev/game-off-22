using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HalfpipeTrigger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera halfpipeCam;

    void OnTriggerEnter(Collider other)
    {
        halfpipeCam.enabled = true;
    }
}
