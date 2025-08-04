using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 5f;
    public float fireRate = 1.5f;
    private float fireTimer = 1.5f;

    public virtual void Attack()
    {
        print("Parent attack!");
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime; // Decrease the timer
        }
        else
        {
            
                Attack(); // Call the attack method
                fireTimer = fireRate; // Reset the timer
            
        }
    }
    
}
