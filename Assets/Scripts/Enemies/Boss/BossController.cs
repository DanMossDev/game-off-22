using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Space][Header("Boss Variables")]
    public float rotationSpeed = 60;
    [Tooltip("Speed at which the boss moves towards the player in spin state")]
    public float rushSpeed = 10;
    [Tooltip("Amount of time the boss spends charging before unleashing the projectile attack")]
    public float chargeTime = 5;

    [Space][Header("Transforms and Prefabs")]
    public GameObject player;
    public GameObject SphereHolder;
    public GameObject[] Spheres;
    public GameObject head;

    [HideInInspector] public Animator animator;

    [HideInInspector] public bool damagedThisCycle = false;
    [HideInInspector] public List<Vector3> sphereStartingPos = new List<Vector3>();
    float lerp;

    [HideInInspector] public BossState currentState;
    [HideInInspector] public BossBase baseState = new BossBase();
    [HideInInspector] public BossSpin spinState = new BossSpin();
    [HideInInspector] public BossCharge chargeState = new BossCharge();
    [HideInInspector] public BossRelease releaseState = new BossRelease();
    [HideInInspector] public BossExhaust exhaustState = new BossExhaust();

    public static BossController Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        foreach (GameObject sphere in Spheres)
        {
            sphereStartingPos.Add(sphere.transform.localPosition);
        }
    }

    void OnEnable()
    {
        currentState = baseState;
        currentState.EnterState(this);
    }

    void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState()
    {
        currentState.LeaveState(this);
        if (currentState != baseState) currentState = baseState;
        else 
        {
            int random = Random.Range(0, 2);
            if (random == 0) currentState = spinState;
            else currentState = chargeState;
        }
        currentState.EnterState(this);
    }

    public void BeginAttack()
    {
        currentState.LeaveState(this);
        currentState = releaseState;
        currentState.EnterState(this);
    }

    public void BecomeExhausted()
    {
        currentState.LeaveState(this);
        currentState = exhaustState;
        currentState.EnterState(this);
    }

    public void restoreSpheres()
    {
        lerp = 0;
        StartCoroutine(EaseSpheres());
    }

    IEnumerator EaseSpheres()
    {
        while (lerp <= 1)
        {
            lerp += Time.deltaTime / 2;
            for (int i = 0; i < Spheres.Length; i++)
            {
                Spheres[i].transform.localPosition = Vector3.Lerp(Spheres[i].transform.localPosition, sphereStartingPos[i], lerp);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void LookAtPlayer()
    {
        Vector3 toPlayer = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(toPlayer, Vector3.up), rotationSpeed * Time.deltaTime);
    }

    public void Die()
    {
        //animator.SetTrigger("Die");
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        LevelManager.Instance.LevelComplete();
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}