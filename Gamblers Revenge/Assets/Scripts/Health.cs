using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic health component that can be attached to any damageable object.
/// Handles taking damage and destroying the object when health reaches zero.
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>Maximum health of the object.</summary>
    public float maxHp = 10f;
    /// <summary>Current health value.</summary>
    public float curHp = 0f;

    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp; // Initialize current health to maximum health
    }

    /// <summary>Reduce health by <paramref name="amt"/> and destroy the
    /// object if it runs out.</summary>
    public void TakeDamage(float amt)
    {
        curHp -= amt; // Decrease current health by the amount of damage taken
        if (curHp <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
