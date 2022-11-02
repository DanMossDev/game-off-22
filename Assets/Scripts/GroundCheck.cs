using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        print("Grounded");
    }
}
