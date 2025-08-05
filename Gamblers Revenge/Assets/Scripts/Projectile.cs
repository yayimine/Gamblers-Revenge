using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public int maxPierces = 1;

    public float lifetime = 3f; // Time before the projectile is destroyed
    private HashSet<Collider2D> _alreadyHit = new HashSet<Collider2D>();
    private int _piercesLeft;


    void Awake()
    {
        _piercesLeft = maxPierces;
    }

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the projectile after a certain time
                                       //rotate opposite to player
        transform.position = new Vector3(transform.position.x, //take the player's x position
transform.position.y, //take the player's y position
PlayerController.instance.transform.position.z); //keep the original z position for the camera
        print("spawned");
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        print("hit");
        if (_piercesLeft <= 0) return;

        // only hit things tagged "Enemy"
        if (!other.CompareTag("Enemy") && !other.CompareTag("Player")) return;
        if (_alreadyHit.Contains(other)) return;

        _alreadyHit.Add(other);
        _piercesLeft--;

        // destroy when out of pierces
        if (_piercesLeft <= 0)
            Destroy(gameObject);
    }
}

