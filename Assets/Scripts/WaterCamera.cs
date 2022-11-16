using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 15, player.transform.position.z - 30);
    }
}
