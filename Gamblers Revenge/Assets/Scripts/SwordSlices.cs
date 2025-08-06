using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlices : Weapon
{
    public GameObject projectilePrefab; //assign this in the inspector
    float shotSpeed = 0f;
    void Start()
    {
        shotSpeed = PlayerController.instance.shotSpeed;
    }
    public override bool Attack()
    {
        if (Input.GetMouseButton(0) == false)
        {
            return false;
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

        Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir.normalized * shotSpeed;
        g.GetComponent<Projectile>().damage = damage;
        return true;
    }

}
