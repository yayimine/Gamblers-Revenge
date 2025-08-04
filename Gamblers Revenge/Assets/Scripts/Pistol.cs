using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public GameObject projectilePrefab; //assign this in the inspector
    public float shotSpeed = 20f;
    public override void Attack()
    {
        //how to shoot to the direction where my mouse is
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 shootDir = 
            mousePos - (Vector2)transform.position;
        //we need to spawn the prefab and store the instantiated instance of the object
        GameObject g = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        g.GetComponent<Rigidbody2D>().velocity = shootDir.normalized * shotSpeed;
        g.GetComponent<Projectile>().damage = damage;
    }
}
