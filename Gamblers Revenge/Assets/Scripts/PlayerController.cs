using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Leveling & Upgrades")]
    public int points = 0;
    public int maxPoints = 5;
    public int level = 1;
    public Weapon curWeapon;
    public GameObject projectilePrefab;

    public UpgradeManager upgradeManager; // assign in Inspector

    private bool _awaitingUpgrade = false;
    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameManager.instance.InitializeScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Loot"))
        {
            Destroy(collision.gameObject);

            if (_awaitingUpgrade) return;

            points++;
            if (points >= maxPoints)
                LevelUp();
        }
    }

    private void LevelUp()
    {
        _awaitingUpgrade = true;

        // 1) Apply your static scaling
        level++;
        points = 0;
        maxPoints = Mathf.RoundToInt(maxPoints * 1.5f);
        FindObjectOfType<SpawnManager>().spawnRate *= 0.8f;

        // 2) Pick 3 random upgrades
        var picks = upgradeManager.GetRandomUpgrades(3);
        var optionNames = picks.Select(u => u.ToString()).ToArray();

        // 3) Show UI
        UIManager.instance.ShowUpgradeScreen(optionNames, choice =>
        {
            // 4) Apply the one upgrade they picked
            var chosenType = picks[choice];
            upgradeManager.ApplyUpgrades(
                new List<UpgradeType> { chosenType },
                weapon: curWeapon,
                player: this,
                projectile: projectilePrefab.GetComponent<Projectile>()
            );

            _awaitingUpgrade = false;
        });
    }
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(horizontalInput, verticalInput).normalized; // .normalized makes the vector have a magnitude of 1! it is important!

        rb.velocity = inputDir * speed;
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.magnitude));

        if (horizontalInput > 0)
        {
            anim.SetBool("MovingRight", true);
        }
        else if (horizontalInput < 0)
        {
            anim.SetBool("MovingRight", false);
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetFloat("speed", speed);  // or use rb.velocity.magnitude
        }
        else
        {
            anim.SetFloat("speed", 0f);
        }
    }
    
    // Example in PlayerController:

}