using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] float boostForce = 80;
    [SerializeField] AudioClip[] boostSound;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * boostForce, ForceMode.VelocityChange);
            SFXController.Instance.PlaySFX(boostSound);
        }

    }
}
