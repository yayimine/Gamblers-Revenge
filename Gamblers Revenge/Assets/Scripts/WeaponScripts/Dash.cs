using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Weapon
{
    // Start is called before the first frame update
    public GameObject dash;
    public Animator anim;
    float slashSpeed = 0f;

    private void Awake()
    {
        damage = 30f;
        fireRate = 6f;
        fireTimer = 6f;
    }

    

    public override void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Attack(); // Call the attack method
        }
        base.Update();

    }


    // Start is called before the first frame update
    public override void Attack()
    {
        if (!(fireTimer < 0))
        {
            return;
        }
        //anim.SetTrigger("SwordSlash");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = mousePos - (Vector2)transform.position;



        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);

        GameObject slash = Instantiate(dash, transform.position, rot);

        Rigidbody2D rb = slash.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir.normalized * slashSpeed;
        slash.GetComponent<Projectile>().damage = damage;


        base.Attack();

    }
}
