using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossController : MonoBehaviour
{
    enum States {
        Idle,
        Spin,
        Shoot
    }

    States state;

    [SerializeField] GameObject[] Targets;
    
    float timeTilNextState = 5;
    float lastChange;


    void Start()
    {
        state = States.Idle;
        lastChange = Time.time;
    }

    void Update()
    {
        switch (state)
        {
            case States.Idle:
                Idle();
                break;
            case States.Spin:
                Spin();
                break;
            case States.Shoot:
                Shoot();
                break;
        }

        if (Time.time - lastChange >= timeTilNextState) ChangeState();
    }

    void ChangeState()
    {
        if (state == States.Idle) state = (States)Random.Range(1, 3);
        else state = States.Idle;

        timeTilNextState = Random.Range(5, 16);
        lastChange = Time.time;
    }

    void Idle()
    {
        print("In idle state");
    }

    void Spin()
    {
        //animator.SetTrigger("Spin");
        print("In spin state");
    }

    void Shoot()
    {
        //animator.SetTrigger("Shoot");
        print("In shoot state");
    }

    public void Die()
    {
        //animator.SetTrigger("Die");
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        LevelManager.Instance.LevelComplete();
        gameObject.SetActive(false);
    }
}