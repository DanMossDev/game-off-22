using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 MoveToHere;
    Vector3 moveTo;
    Vector3 moveFrom;
    float lerp = 0;
    bool movingTo = true;

    void Start()
    {
        moveFrom = transform.position;
        moveTo = transform.position + MoveToHere;
        movingTo = true;
    }

    void FixedUpdate()
    {
        if (movingTo) lerp += Time.deltaTime * moveSpeed;
        else lerp -= Time.deltaTime * moveSpeed;

        transform.position = Vector3.Lerp(moveFrom, moveTo, lerp);

        if (lerp >= 1 || lerp <= 0) movingTo = !movingTo;
    }

    void OnCollisionEnter(Collision other)
    {
        other.gameObject.transform.parent = transform;
    }

    void OnCollisionExit(Collision other)
    {
        other.gameObject.transform.parent = null;
    }
}
