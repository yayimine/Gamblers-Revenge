using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float lifetime = 3f; // Time before the projectile is destroyed
    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the projectile after a certain time
                                       //rotate opposite to player
                                       transform.position = new Vector3(transform.position.x, //take the player's x position
           transform.position.y, //take the player's y position
            PlayerController.instance.transform.position.z); //keep the original z position for the camera
        
    }
}
