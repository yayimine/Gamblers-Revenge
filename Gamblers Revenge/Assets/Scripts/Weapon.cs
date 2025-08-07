using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1f;
    public float fireRate = 1.5f;
    public float fireTimer = 1.5f;

    public virtual void Attack()
    {
        fireTimer = fireRate; // Reset the timer
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime; // Decrease the timer
        }
        else
        {
            
                
            
        }
       
    }
    
}
