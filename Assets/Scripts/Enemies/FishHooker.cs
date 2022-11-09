using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHooker : MonoBehaviour
{
    [Tooltip("Range in which the player can be detected")][SerializeField] 
    float agroRange = 15;
    [Tooltip("Time taken between spotting the player and attacking")][SerializeField] 
    float shotCD = 1.5f;
    [SerializeField] GameObject[] hooks;
    [SerializeField] LayerMask playerLayer;

    public GameObject target;
    float lastShotTime;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, agroRange, playerLayer);
        if (player.Length != 0)
        {
            if (target == null) lastShotTime = Time.time;
            target = player[0].gameObject;
        } else target = null;
        if (target != null)
        {
            Rotate();

            if (Time.time - lastShotTime >= shotCD)
            {
                for (int i = 0; i < hooks.Length; i++)
                {
                    if (!hooks[i].activeSelf)
                    {
                        hooks[i].SetActive(true);
                        Vector3 targetVel = target.GetComponentInParent<Rigidbody>().velocity;
                        hooks[i].GetComponent<HookShot>().target = target.transform.position + (Vector3.ClampMagnitude(new Vector3(targetVel.x, targetVel.y / 5, targetVel.z), 30)) + Vector3.up;
                        lastShotTime = Time.time;
                        break;
                    }
                }
            }
            return;
        }


    }

    void Rotate()
    {
        Vector3 toPlayer = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(toPlayer, Vector3.up), 360 * Time.deltaTime);
    }
}
