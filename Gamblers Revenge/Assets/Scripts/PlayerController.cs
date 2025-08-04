using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; //singleton

    public float speed = 5f;
    Rigidbody2D rb;

    public int points = 0;
    public int maxPoints = 5;
    public int level = 1;
    public Weapon curWeapon; // set the pistol from the hierarchy to this slot in the inspector


    private void OnDestroy()
    {
        UIManager ui = FindObjectOfType<UIManager>();
        ui.loseScreen.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        { //the player is touching the loot object
            Destroy(collision.gameObject);

            points += 1;
            if(points >= maxPoints)
            {
                level += 1;
                points = 0;
                maxPoints += 1;
                curWeapon.fireRate /= 1.5f;

                SpawnManager s = FindObjectOfType<SpawnManager>();
                s.spawnRate *= .8f;
            }
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;

        GameManager.instance.InitializeScore(); // Initialize the score in GameManager
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
      
        Vector2 inputDir = new Vector2(horizontalInput, verticalInput).normalized; // .normalized makes the vector have a magnitude of 1! it is important!

        rb.velocity = inputDir * speed;
    }
}