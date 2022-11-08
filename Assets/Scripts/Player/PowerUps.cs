using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [Space][Header("Energy Drink Variables")]
    [SerializeField] float drinkTime = 3;
    [SerializeField] float drinkForce = 15;
    [Space][Header("Energy Drink References")]
    [SerializeField] GameObject EnergyDrink;
    [SerializeField] GameObject Toast;
    [SerializeField] SkinnedMeshRenderer body;

    Mesh bodyMesh;

    bool hasDrink = false;

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
        bodyMesh = body.sharedMesh;
    }

    public void PowerUp(Pickups powerUp)
    {
        switch (powerUp)
        {
            case Pickups.Toast:

                powerUpStartTime = Time.time;
                if (hasDrink) break;
                else StartCoroutine(DrinkEnergy());
                break;
            default:
                break;
        }
    }

    public void StopEnergyDrink()
    {
        EnergyDrink.SetActive(false);
        Toast.SetActive(true);
        body.SetBlendShapeWeight(0, 0);
        StopAllCoroutines();
    }

    IEnumerator DrinkEnergy()
    {
        EnergyDrink.SetActive(true);
        Toast.SetActive(false);
        body.SetBlendShapeWeight(bodyMesh.GetBlendShapeIndex("MouthOpen"), 100);
        hasDrink = true;
        while (Time.time < powerUpStartTime + drinkTime)
        {
            yield return new WaitForEndOfFrame();
            rigidBody.AddForce(transform.forward * drinkForce, ForceMode.Force);
        }
        while (Time.time < powerUpStartTime + drinkTime + 1f)
        {
            yield return new WaitForEndOfFrame();
            rigidBody.velocity = new Vector3(rigidBody.velocity.x * 0.99f, rigidBody.velocity.y, rigidBody.velocity.z * 0.99f);
        }
        hasDrink = false;
        EnergyDrink.SetActive(false);
        Toast.SetActive(true);
        body.SetBlendShapeWeight(0, 0);
    }
}
