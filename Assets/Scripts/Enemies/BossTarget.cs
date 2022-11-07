using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTarget : MonoBehaviour
{
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void OnHit()
    {
        gameObject.layer = 0;
        meshRenderer.enabled = false;
        StartCoroutine(Restore());
    }

    IEnumerator Restore()
    {
        yield return new WaitForSeconds(3);
        gameObject.layer = LayerMask.NameToLayer("Target");
        meshRenderer.enabled = true;
    }
}
