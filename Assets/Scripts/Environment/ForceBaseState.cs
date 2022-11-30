using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBaseState : MonoBehaviour
{
    void OnTriggerEnter(Collider other) => PlayerController.Instance.ChangeState(PlayerController.Instance.baseState);
}
