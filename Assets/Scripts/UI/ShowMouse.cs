using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMouse : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
