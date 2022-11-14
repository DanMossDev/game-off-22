using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] Doors;
    [SerializeField] GameObject[] Enemies;
    bool isTriggered = false;
    void FixedUpdate()
    {
        if (!isTriggered) return;

        foreach (GameObject enemy in Enemies)
        {
            if (enemy.activeSelf)
            {
                return;
            }
            else
            {
                foreach (GameObject door in Doors)
                {
                    door.SetActive(false);
                }

                gameObject.SetActive(false);
            }
        }
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
