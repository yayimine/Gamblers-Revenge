using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlices : Weapon
{
    public AudioSource BladeSlash;
    public GameObject projectilePrefab; //assign this in the inspector
    float shotSpeed = 20f;
    public float volume=5;
    private void Awake()
    {
        damage = 10f;
        fireRate = 0.5f;
        fireTimer = 0.5f;
    }
    public override void Attack()
    {
        damage = PlayerController.instance.damage * 10f;
        if (!(fireTimer < 0))
        {
            return;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = mousePos - (Vector2)transform.position;

        // compute world‐space angle of the shot vector
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

        // subtract 90° so that your “up”-oriented sprite faces along shootDir
        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);

        GameObject g = Instantiate(
            projectilePrefab,
            transform.position,
            rot
        );
        BladeSlash.PlayOneShot(BladeSlash.clip, volume);

        Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir.normalized * shotSpeed;
        g.GetComponent<Projectile>().damage = damage;

        base.Attack();


    }

    public override void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            Attack(); // Call the attack method
        }
        base.Update();
    }
    
}
