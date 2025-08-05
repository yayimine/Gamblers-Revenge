using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { //if the enemy collides with the player
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

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













