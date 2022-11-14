using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCollider : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    MeshCollider meshCollider;
    float time;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        time = 0;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= 0.1f)
        {
            time = 0;
            SetCollider();
        }
    }

    
    void SetCollider() 
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = colliderMesh;
     }
}
