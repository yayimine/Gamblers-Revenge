using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public GameObject swordSlash;
    public float slashSpeed = 30f;
    // Start is called before the first frame update
    public override void Attack()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = mousePos - (Vector2)transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
