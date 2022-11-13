using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("upward force applied to bean")]
    [SerializeField] float upForce = 10f;

    [Tooltip("Average number of beans shot per second")]
    [SerializeField] float forceSpread = 2f;

    void Start()
    {
        if (gameObject.name.Contains("(Clone)")){
            Destroy(gameObject, 2f);
        }
        // set velocity 
        float forcexz = Random.Range(-forceSpread, forceSpread);
        float forceup = upForce + Random.Range(-forceSpread, forceSpread);
        
        GetComponent<Rigidbody>().AddForce(forcexz, forceup, forcexz, ForceMode.Impulse);

        // set rotation
        GetComponent<Rigidbody>().AddTorque(
                Random.Range(-forceSpread, forceSpread)/10, 
                Random.Range(-forceSpread, forceSpread)/10, 
                Random.Range(-forceSpread, forceSpread)/10, 
                ForceMode.Impulse
                );


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(other);
        }    
    }

    void OnDestroy() 
    {

    }
}
