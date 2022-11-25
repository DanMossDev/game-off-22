using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Halfpipe : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart dolly;

    [SerializeField] Transform centralPoint;
    float lerp = 0;
    void OnCollisionEnter(Collision other)
    {
        PlayerController.Instance.pipeCenter = centralPoint;
        other.gameObject.GetComponent<PlayerController>().ChangeState(PlayerController.Instance.pipeState);

        other.gameObject.transform.parent = dolly.transform;
        dolly.m_Speed = 80;
        PlayerController.Instance.animator.SetFloat("moveSpeed", Mathf.Log(80 / 10 + 1));
    }
}
