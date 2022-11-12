using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Halfpipe : MonoBehaviour
{
    [SerializeField] float dollySpeed = 80;
    [SerializeField] CinemachineDollyCart dolly;
    float lerp = 0;
    void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<PlayerController>().ChangeState(PlayerController.Instance.pipeState);

        other.gameObject.transform.parent = dolly.transform;
        dolly.m_Speed = dollySpeed;
    }
}
