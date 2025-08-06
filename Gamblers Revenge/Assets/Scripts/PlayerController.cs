using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Leveling & Upgrades")]
    public int points = 0;
    public int maxPoints = 5;
    public int level = 1;

    [Header("Movement & Combat")]
    public float speed = 7f;
    public Weapon curWeapon;
    public GameObject projectilePrefab;

    [Header("Stats (modified by upgrades)")]
    public float damage = 1f;
    public float shotSpeed = 20f;
    public int maxPierces = 1;

    [Header("References")]
    public static PlayerController instance;

    private bool _awaitingUpgrade = false;
    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        // singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb   = GetComponent<Rigidbody2D>();
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

        // 1) static scaling
        level++;
        points = 0;
        maxPoints = Mathf.RoundToInt(maxPoints * 1.5f);
        FindObjectOfType<SpawnManager>().spawnRate *= 0.8f;

        // 2) pick 3 random upgrades
        List<UpgradeEffects.UpgradeType> picks = UpgradeEffects.instance.ChooseUpgrades();
        string[] optionNames = picks.Select(u => u.ToString()).ToArray();

        // 3) show UI
        UIManager.instance.ShowUpgradeScreen(optionNames, choice =>
        {
            // 4) apply the picked upgrade
            UpgradeEffects.UpgradeType chosen = picks[choice];
            UpgradeEffects.instance.ApplyUpgrade(chosen);

            _awaitingUpgrade = false;
        });
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;

        rb.velocity = dir * speed;
        anim.SetFloat("speed", rb.velocity.magnitude);

        if (h > 0)      anim.SetBool("MovingRight", true);
        else if (h < 0) anim.SetBool("MovingRight", false);
    }
}
