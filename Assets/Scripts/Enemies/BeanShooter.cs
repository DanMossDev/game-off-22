using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// random number



public class BeanShooter : MonoBehaviour
{

    
    float nextShot = 0;
    float nextShotDelay;
    [Tooltip("Average number of beans shot per second")]
    [SerializeField] float beanFrequency = 5f;
    [Tooltip("Randomness of bean spawn frequency")]
    [SerializeField] float beanSpread = 0.2f;
    
    [SerializeField] GameObject BeanOrigin;
    [SerializeField] GameObject Bean;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShot) {
            nextShotDelay = Random.Range( (1/beanFrequency) - beanSpread, (1/beanFrequency) + beanSpread );
            nextShot = Time.time + nextShotDelay;
            ShootBean(nextShotDelay);
        }
       
    }

    //
    void ShootBean(float nextShotDelay)
    {
        // to do: make this use an object pool
        GameObject newBean = Instantiate(Bean, BeanOrigin.transform.position, Quaternion.identity);        
    }
}
