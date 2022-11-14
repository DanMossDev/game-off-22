using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Transform cameraTrans;

    void Start()
    {
        cameraTrans = Camera.main.transform;
    }
    void Update()
    {
        transform.rotation = cameraTrans.rotation;
    }
}
