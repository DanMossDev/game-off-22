using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] int MaxHP = 3;
    [HideInInspector] public int currentHP;

    [Space][Header("Audio")]
    [SerializeField] AudioClip[] deathSound;

    void Start()
    {
        currentHP = MaxHP;
    }
    public void TakeDamage()
    {
        currentHP--;

        if (currentHP <= 0) Die();
    }

    public void Heal()
    {
        if (currentHP < MaxHP) currentHP++;
    }

    void Die()
    {
        //SFXController.Instance.PlaySFX(deathSound);
        if (gameObject.name == "Player") LevelManager.Instance.GameOver(GameOvers.Death);

        else {
            Destroy(gameObject);
            LevelManager.Instance.Score += 200;
        }
    }
}
