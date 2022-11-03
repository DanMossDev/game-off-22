using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Pickup : MonoBehaviour
{
    CapsuleCollider capsuleCollider;
    public Pickups pickup;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) HandlePickup();
    }

    void HandlePickup()
    {
        switch (pickup)
        {
            case Pickups.Toast:
                PowerUps.Instance.PowerUp(Pickups.Toast);
                break;
            default:
                break;
        }
    }
}

public enum Pickups {
    Toast
}
