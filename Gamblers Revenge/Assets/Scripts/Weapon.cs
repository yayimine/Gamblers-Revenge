using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base weapon class that handles firing rate and calls an overridable Attack
/// method when ready. Specific weapons derive from this class.
/// </summary>
public class Weapon : MonoBehaviour
{
    public float damage = 1f;
    public float fireRate = 1.5f;
    public float fireTimer = 1.5f;

    /// <summary>
    /// Perform the weapon's attack. Returns true if an attack was executed so
    /// the fire timer can be reset.
    /// </summary>
    public virtual bool Attack()
    {
        print("Parent attack!");
        return true;
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
            if (Attack()) // Call the attack method
            {
                fireTimer = fireRate; // Reset the timer only if attack was performed
            }
        }

    }

}
