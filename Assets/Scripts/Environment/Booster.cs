using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [Space][Header("Boost Variables")]
    [SerializeField] float boostForce = 80;
    [SerializeField] float boostTime = 0;
    [Space][Header("Audio")]
    [SerializeField] AudioClip[] boostSound;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerController player = other.GetComponent<PlayerController>();
            if (boostTime > 0)
            {
                player.boostTime = boostTime;
                player.ChangeState(player.boostState);
                other.GetComponent<Rigidbody>().velocity = transform.forward * boostForce;
            }
            else other.GetComponent<Rigidbody>().AddForce(transform.forward * boostForce, ForceMode.VelocityChange);
            SFXController.Instance.PlaySFX(boostSound);
        }

    }
}
