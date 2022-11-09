using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject player;

    void Update()
    {
        Vector3 direction = new Vector3(boss.transform.position.x - player.transform.position.x, -2, boss.transform.position.z - player.transform.position.z).normalized;

        transform.position = player.transform.position - (direction * distance);
    }
}
