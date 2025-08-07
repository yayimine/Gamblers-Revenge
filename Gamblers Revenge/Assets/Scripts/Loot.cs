using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Collectable loot orb that homes toward the player when they are within a
/// specified range.  Once collected it is destroyed by the player script.
/// </summary>
public class Loot : MonoBehaviour
{
    [Header("Homing Settings")]
    /// <summary>Range within which the orb will start homing towards the player.</summary>
    public float homingRange = 5f;

    private Transform player;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj != null ? playerObj.transform : null;
    }

    void FixedUpdate()
    {
        // Acquire player reference if we somehow lost it.
        if (player == null)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            player = playerObj != null ? playerObj.transform : null;
            if (player == null) return; // If the player doesn't exist, do nothing
        }
        Vector2 orbPos = transform.position;
        Vector2 playerPos = player.position;
        playerPos += Vector2.down; // Offset player position slightly to avoid z-fighting
        float dist = Vector2.Distance(orbPos, playerPos);

        if (dist <= homingRange)
        {
            // Move toward the player. Speed increases as distance decreases.
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
