using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Pickup : MonoBehaviour
{  
    [Tooltip("Which pickup this is an instance of")]
    public Pickups pickup;
    [Tooltip("An array of possible audio played by this pickup")][SerializeField]
    AudioClip[] pickupAudio;


    CapsuleCollider capsuleCollider;

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
                LevelManager.Instance.Score += 100;
                PowerUps.Instance.PowerUp(Pickups.Toast);
                break;
            default:
                break;
        }

        // SFXController.Instance.PlaySFX(pickupAudio)
        Destroy(this.gameObject);
    }
}

public enum Pickups {
    Toast
}
