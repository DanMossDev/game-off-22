using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (((-p0 + 3*(p1-p2) + p3)* t + (3*(p0+p2) - 6*p1))* t + 3*(p1-p0))* t + p0;
    }
}
