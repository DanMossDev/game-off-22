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

    void OnTriggerEnter(Collider other) 
    {
        other.gameObject.transform.SetParent(transform);
    }

    void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.SetParent(null);
        Vector3 direction;
        if (movingTo) direction = moveTo - moveFrom;
        else direction = moveFrom - moveTo;

        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, direction.y * moveSpeed / 2, 0), ForceMode.Impulse);
    }
}
