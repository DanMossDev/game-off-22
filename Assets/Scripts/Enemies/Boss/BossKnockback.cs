using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnockback : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().ApplyHitstun(new Vector3(other.contacts[0].normal.x * 3, other.contacts[0].normal.y, other.contacts[0].normal.z * 3).normalized * -1);

            BossController.Instance.animator.SetTrigger("hit");

            for (int i = 0; i < BossController.Instance.Spheres.Length; i++)
            {
                //TYLER
                BossController.Instance.Spheres[i].GetComponentInChildren<Animator>().SetTrigger("uncurl");
            }
        }
    }
}
