using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float lifetime = 3f; // Time before the projectile is destroyed
    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the projectile after a certain time
    }
}
