using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RecenterCamera : MonoBehaviour
{
    SimpleFollowRecenter recenter;
    public static RecenterCamera Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        recenter = GetComponent<SimpleFollowRecenter>();
    }

    public void Recenter()
    {
        recenter.Recenter = true;
    }

    public void StopRecentering()
    {
        recenter.Recenter = false;
    }
}
