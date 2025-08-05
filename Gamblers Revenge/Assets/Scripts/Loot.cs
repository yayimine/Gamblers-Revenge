using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    // Update is called once per frame
   
    [Header("Homing Settings")]
    public float homingRange = 5f;           // range within which the orb will start homing towards the player
    public float attractionSpeed = 3f;       // speed at which the orb moves towards the player

    [Header("Optional Speed Clamping")]
    public bool clampSpeed = false;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;

    private Transform player;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector2 orbPos = transform.position;
        Vector2 playerPos = player.position;
        float dist = Vector2.Distance(orbPos, playerPos);

        if (dist <= homingRange)
        {
            Vector2 dir = (playerPos - orbPos).normalized;

            float speed = attractionSpeed;

            if (clampSpeed)
            {
                // Optionally clamp between min and max
                speed = Mathf.Clamp(attractionSpeed, minSpeed, maxSpeed);
                // Or if you prefer distance‐based interpolation:
                // speed = Mathf.Lerp(minSpeed, maxSpeed, dist / homingRange);
            }

            rb.velocity = dir * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
