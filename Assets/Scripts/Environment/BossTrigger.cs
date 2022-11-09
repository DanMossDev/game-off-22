using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject[] bossWalls;
    [SerializeField] CinemachineFreeLook followCam;
    [SerializeField] CinemachineVirtualCamera bossCam;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            LevelManager.Instance.StopTimer();
            boss.SetActive(true);
            followCam.enabled = false;
            bossCam.enabled = true;
            foreach(GameObject wall in bossWalls) wall.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
