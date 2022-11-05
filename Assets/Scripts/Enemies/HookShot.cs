using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    [System.NonSerialized] public Vector3 p3;
    Vector3 p0;
    Vector3 p1;
    Vector3 p2;

    float lerp = 0;

    void OnEnable()
    {
        transform.position = transform.parent.transform.position;
        lerp = 0;
        p0 = transform.position;
        p1 = transform.position + (Vector3.up * 5);
        p2 = p3 + (Vector3.up * 5);
    }
    void Update()
    {
        lerp += Time.deltaTime;
        transform.position = Utils.cubeBezier3(p0, p1, p2, p3, lerp);

        if (lerp >= 1) BecomeInactive();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) other.gameObject.GetComponent<PlayerController>().TakeDamage(other);
        BecomeInactive();
    }

    void BecomeInactive()
    {
        GetComponentInParent<FishHooker>().target = null;
        gameObject.SetActive(false);
    }
}
