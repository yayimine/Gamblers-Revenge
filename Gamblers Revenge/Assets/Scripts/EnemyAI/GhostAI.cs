using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { //if the enemy collides with the player
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }
    [Header("Loot Settings")]
    public int lootValue = 1; // Value of the loot dropped by the enemy
    public float lootScale = 1f; // Scale of the loot when spawned
    public GameObject loot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        { //if enemy collides with projectile
            
            GetComponent<Health>().TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            if (GetComponent<Health>().curHp <= 0f)
            {
                Destroy(gameObject);
                if (loot != null)
                {
                    //loot scale = 1
                    loot.transform.localScale = new Vector3(lootScale, lootScale, 1f); // Set the scale of the loot
                    Loot lootComponent = loot.GetComponent<Loot>();
                    if (lootComponent != null)
                    {
                        lootComponent.value = lootValue; // Set the value of the loot
                    }
                    Instantiate(loot, transform.position, loot.transform.rotation); // Spawn loot at the enemy's position
                }

                GameManager.instance.UpdateScore(1); // Add score when the enemy is destroyed
            }
        } else if (collision.gameObject.CompareTag("Slash"))
        { //if enemy collides with projectile

            GetComponent<Health>().TakeDamage(collision.gameObject.GetComponent<Slash>().damage);
            if (GetComponent<Health>().curHp <= 0f)
            {
                Destroy(gameObject);
                if (loot != null)
                {
                    //loot scale = 1
                    loot.transform.localScale = new Vector3(lootScale, lootScale, 1f); // Set the scale of the loot
                    Loot lootComponent = loot.GetComponent<Loot>();
                    if (lootComponent != null)
                    {
                        lootComponent.value = lootValue; // Set the value of the loot
                    }
                    Instantiate(loot, transform.position, loot.transform.rotation); // Spawn loot at the enemy's position
                }

                GameManager.instance.UpdateScore(1); // Add score when the enemy is destroyed
            }
        }
    }


    //move to the player
    private Rigidbody2D rb;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    
        
    void Update()
    {
        if(PlayerController.instance == null) return; // If the player doesn't exist, do nothing
        
        //Direction Vector = Target Position - Current Position (THIS GOING TOWARDS THE TARGET)
        //Direction Vector = Current Position - Target Position (THIS GOING AWAY FROM THE TARGET)

        Vector2 dir = PlayerController.instance.transform.position - transform.position;
        rb.velocity = dir.normalized * speed;
    }

}













