using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooch : MonoBehaviour
{
    [System.NonSerialized] public Vector3 target;
    Vector3 startPos;

    float timeShot;

    void OnEnable()
    {
        if (transform.parent == null) 
        {
            BecomeInactive();
            return;
        }
        transform.position = startPos = transform.parent.transform.position;
        timeShot = Time.time;
    }
    void Update()
    {
        MoveTowardsTarget();
        if (Time.time - timeShot >= 5f) BecomeInactive();
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target - startPos).normalized;
        transform.position += direction * Time.deltaTime * 40;
    }
        

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) other.gameObject.GetComponent<PlayerController>().TakeDamage(other);
        BecomeInactive();
    }

    void BecomeInactive()
    {
        gameObject.SetActive(false);
    }
}
