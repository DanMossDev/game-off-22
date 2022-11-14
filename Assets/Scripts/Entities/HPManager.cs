using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] int MaxHP = 1;
    [SerializeField] bool isPlayer = false;
    [SerializeField] bool isBoss = false;
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
        if (currentHP <= 0) 
        {
            Die();
            return;
        }
        if (isBoss) GetComponentInParent<BossController>().damagedThisCycle = true;
    }

    public void Heal()
    {
        if (currentHP < MaxHP) currentHP++;
    }

    public void Die()
    {
        SFXController.Instance.PlaySFX(deathSound);
        if (isPlayer) LevelManager.Instance.GameOver(GameOvers.Death);
        else if (isBoss) {
            GetComponentInParent<BossController>().Die();
        }
        else {
            transform.parent.gameObject.SetActive(false);
            LevelManager.Instance.Score += 200;
        }
    }
}
