using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] Doors;
    [SerializeField] GameObject[] Enemies;
    bool isTriggered = false;
    bool allDead = true;
    void FixedUpdate()
    {
        if (!isTriggered) return;

        CheckIfDead();

        if (allDead) OpenDoor();
    }

    void CheckIfDead()
    {
        allDead = true;
        foreach (GameObject enemy in Enemies)
        {
            if (enemy.activeSelf)
            {
                allDead = false;
                return;
            }
        }
    }

    void OpenDoor()
    {
        foreach (GameObject door in Doors)
        {
            door.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) 
    {
        foreach (GameObject door in Doors)
        {
            door.SetActive(true);
        }
        isTriggered = true;
    }
}
