using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic behaviour for ghost enemies that chase the player and take damage from
/// projectiles. When destroyed they can drop loot and award score.
/// </summary>
public class GhostAI : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { // if the enemy collides with the player
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

    public GameObject loot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        { // if enemy collides with projectile

            GetComponent<Health>().TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            if (GetComponent<Health>().curHp <= 0f)
            {
                Destroy(gameObject);
                if (loot != null)
                {
                    Instantiate(loot, transform.position, loot.transform.rotation); // Spawn loot at the enemy's position
                }

                GameManager.instance.UpdateScore(1); // Add score when the enemy is destroyed
            }
        }
    }

    // move to the player
    private Rigidbody2D rb;
    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(PlayerController.instance == null) return; // If the player doesn't exist, do nothing

        // Direction vector points from enemy to player
        Vector2 dir = PlayerController.instance.transform.position - transform.position;
        rb.velocity = dir.normalized * speed;
    }

}













