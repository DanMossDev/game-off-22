using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject[] bossWalls;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            boss.SetActive(true);
            foreach(GameObject wall in bossWalls) wall.SetActive(true);
            this.enabled = false;
        }
    }
}
