using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic projectile behaviour that travels forward and optionally pierces
/// multiple enemies before being destroyed.
/// </summary>
public class Projectile : MonoBehaviour
{
    // make these public so other scripts (like Sword) can assign to them
    [HideInInspector] public float damage;
    [HideInInspector] public int maxPierces;

    [HideInInspector] public float initialSpeed = 20f;
    public float lifetime = 3f;

    private HashSet<Collider2D> _alreadyHit = new HashSet<Collider2D>();
    private int _piercesLeft;

    void Awake()
    {
        // if nobody set them externally, fall back to the player's defaults
        if (damage <= 0f)
            damage = PlayerController.instance.damage;

        if (maxPierces <= 0)
            maxPierces = PlayerController.instance.maxPierces;

        _piercesLeft = maxPierces;
        initialSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);

        // keep the same z-depth as the player (if you really need it)
        Vector3 pos = transform.position;
        pos.z = PlayerController.instance.transform.position.z;
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }

        if (!other.CompareTag("Enemy") || _alreadyHit.Contains(other))
            return;

        _alreadyHit.Add(other);

        // apply `damage` to the enemy here, e.g.:
        // other.GetComponent<Enemy>()?.TakeDamage(damage);

        if (--_piercesLeft <= 0)
            Destroy(gameObject);
    }
}
