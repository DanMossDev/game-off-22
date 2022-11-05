using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHooker : MonoBehaviour
{
    [Tooltip("Range in which the player can be detected")][SerializeField] 
    float agroRange = 15;
    [Tooltip("Time taken between spotting the player and attacking")][SerializeField] 
    float reelUpTime = 1;
    [SerializeField] GameObject hook;
    [SerializeField] LayerMask playerLayer;

    public GameObject target;
    float timeOfTarget;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (hook.activeSelf) return;
        if (target != null)
        {
            Rotate();

            if (Time.time - timeOfTarget >= reelUpTime)
            {
                hook.SetActive(true);
                hook.GetComponent<HookShot>().p3 = target.transform.position + (target.GetComponent<Rigidbody>().velocity * 1) + Vector3.up;
            }
            return;
        }


        Collider[] player = Physics.OverlapSphere(transform.position, agroRange, playerLayer);
        if (player.Length != 0)
        {
            target = player[0].gameObject;
            timeOfTarget = Time.time;
        }
    }

    void Rotate()
    {

    }
}
