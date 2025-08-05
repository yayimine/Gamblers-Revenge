using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Loot : MonoBehaviour
{
    // Update is called once per frame

    [Header("Homing Settings")]
    public float homingRange = 5f;           // range within which the orb will start homing towards the player

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
        playerPos += Vector2.down; // Offset player position slightly to avoid z-fighting
        float dist = Vector2.Distance(orbPos, playerPos);

        if (dist <= homingRange)
        {
            Vector2 dir = (playerPos - orbPos).normalized;
            
            float speed = (homingRange) / dist; // Interpolate speed based on distance            

            rb.velocity = dir * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
