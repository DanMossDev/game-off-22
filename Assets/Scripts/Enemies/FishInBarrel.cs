using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInBarrel : MonoBehaviour
{
    [Tooltip("Range in which the player can be detected")][SerializeField] 
    float agroRange = 15;
    [Tooltip("Time taken between spotting the player and attacking")][SerializeField] 
    float shotCD = 1.5f;
    [Tooltip("Offset between the attack animation and the attack itself")][SerializeField] 
    float animOffset = 0.2f;
    [SerializeField] GameObject[] kisses;
    [SerializeField] LayerMask playerLayer;

    public GameObject target;
    float lastShotTime;

    Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    void FixedUpdate()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, agroRange, playerLayer);
        if (player.Length != 0)
        {
            if (target == null) lastShotTime = Time.time;
            target = player[0].gameObject;
            animator.SetBool("inRange", true);
        } else {
            target = null;
            animator.SetBool("inRange", false);
        }

        if (target != null)
        {
            Rotate();

            if (Time.time - lastShotTime >= shotCD)
            {
                for (int i = 0; i < kisses.Length; i++)
                {
                    if (!kisses[i].activeSelf)
                    {
                        lastShotTime = Time.time + animOffset;
                        StartCoroutine(ShootKiss(kisses[i]));
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

    IEnumerator ShootKiss(GameObject kiss)
    {
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(animOffset);
        if (target != null)
        {
            kiss.SetActive(true);
            Vector3 targetVel = target.GetComponentInParent<Rigidbody>().velocity;
            kiss.GetComponent<Smooch>().target = target.transform.position + (Vector3.ClampMagnitude(new Vector3(targetVel.x, targetVel.y / 5, targetVel.z), 30)) + Vector3.up;
        }
    }
}
