using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple melee weapon that spawns a sword slash projectile in the direction
/// of the mouse when the player right-clicks.  Inherits firing behaviour from
/// <see cref="Weapon"/>.
/// </summary>
public class Sword : Weapon
{
    /// <summary>Prefab for the slash projectile.</summary>
    public GameObject swordSlash;
    /// <summary>Cached movement speed for the slash projectile.</summary>
    float slashSpeed = 0f;
    /// <summary>Animator used for possible sword animations.</summary>
    public Animator anim;

    private void Awake()
    {
        // Set up default weapon stats.
        damage = 5f;
        fireRate = 3f;
        fireTimer = 3f;
    }

    void Start()
    {
        // Calculate projectile speed based on player's shot speed.
        slashSpeed = PlayerController.instance.shotSpeed * 2;
    }

    /// <summary>
    /// Spawns a slash in the direction of the mouse if the right mouse button
    /// is held. Returns true if an attack occurred so the base class can reset
    /// the fire timer.
    /// </summary>
    public override bool Attack()
    {
        if (Input.GetMouseButton(1) == false)
        {
            return false;
        }
        // Calculate direction from weapon to mouse position.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = mousePos - (Vector2)transform.position;

        // Determine rotation so the slash faces the target direction.
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);

        // Spawn the slash and propel it forward.
        GameObject slash = Instantiate(swordSlash, transform.position, rot);
        Rigidbody2D rb = slash.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir.normalized * slashSpeed;
        slash.GetComponent<Projectile>().damage = damage;

        return true;
    }
}
