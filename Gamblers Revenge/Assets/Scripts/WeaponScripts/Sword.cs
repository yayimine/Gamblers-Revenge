using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : Weapon
{
    public AudioSource Attackslash;
    public GameObject swordSlash;
    float slashSpeed = 40f; 
    public Animator anim;
    public float volume = 5;

    private void Awake()
    {
        damage = 50f;
        fireRate = 3f;
        fireTimer = 3f;
    }

    void Start()
    {
        
    }

    public override void Update()
    {

        if (Input.GetMouseButton(1) == true)
        {
            Attack(); // Call the attack method
        }
        base.Update();
        
    }


    // Start is called before the first frame update
    public override void Attack()
    {
        if ((fireTimer > 0))
        {
            return;
        }
        //anim.SetTrigger("SwordSlash");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = mousePos - (Vector2)transform.position;



        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);

        GameObject slash = Instantiate(swordSlash, transform.position, rot);

        Rigidbody2D rb = slash.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir.normalized * slashSpeed;
        slash.GetComponent<Slash>().damage = damage;


        base.Attack();

        slashSpeed = PlayerController.instance.shotSpeed*2;
        Attackslash.PlayOneShot(Attackslash.clip,volume);
    }
}
