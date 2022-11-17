using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float patrolSpeed = 5;
    public Transform[] patrolPoints;
    int nextPosition = 0;

    void Start()
    {
        nextPosition = 0;
    }
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, patrolPoints[nextPosition].position) < 1f)
        {
            nextPosition = nextPosition == patrolPoints.Length - 1 ? 0 : nextPosition + 1;
        }
        transform.Translate((patrolPoints[nextPosition].position - transform.position).normalized * patrolSpeed * Time.deltaTime);
    }
}
