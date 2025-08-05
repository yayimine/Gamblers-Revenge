using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public GameObject swordSlash;
    public float slashSpeed = 40f;

    public Animator anim;

    private void Awake()
    {
        damage = 5f;
        fireRate = 3f;
        fireTimer = 3f;
    }


    // Start is called before the first frame update
    public override void Attack()
    {
        if (Input.GetMouseButton(1) == false)
        {
            return;
        }
        anim.SetTrigger("SwordSlash");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = mousePos - (Vector2)transform.position;



        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);

        GameObject slash = Instantiate(swordSlash, transform.position, rot);

        Rigidbody2D rb = slash.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir.normalized * slashSpeed;
        slash.GetComponent<Projectile>().damage = damage;

        


    }
}
