using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnockback : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().ApplyHitstun(other.contacts[0].normal * -1);
        }
    }
}
