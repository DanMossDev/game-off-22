using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [Space][Header("Toast Variables")]
    [SerializeField] float toastTime = 3;
    [SerializeField] float toastForce = 15;
    bool hasToast = false;

    float powerUpStartTime;

    Rigidbody rigidBody;

    public static PowerUps Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void PowerUp(Pickups powerUp)
    {
        switch (powerUp)
        {
            case Pickups.Toast:

                powerUpStartTime = Time.time;
                if (hasToast) break;
                else StartCoroutine(EatToast());
                break;
            default:
                break;
        }
    }

    IEnumerator EatToast()
    {
        hasToast = true;
        while (Time.time < powerUpStartTime + toastTime)
        {
            yield return new WaitForEndOfFrame();
            rigidBody.AddForce(transform.forward * toastForce, ForceMode.Force);
        }
        while (Time.time < powerUpStartTime + toastTime + 1f)
        {
            yield return new WaitForEndOfFrame();
            rigidBody.velocity = new Vector3(rigidBody.velocity.x * 0.975f, rigidBody.velocity.y, rigidBody.velocity.z * 0.99f);
        }
        hasToast = false;
    }
}
