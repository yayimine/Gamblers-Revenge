using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using System.Threading.Tasks;


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
    public float projectileScale = 1f; // Scale of the projectile, used for visual size

    [Header("Stats (modified by upgrades)")]
    public float damage = 1f;
    public float shotSpeed = 20f;
    public int maxPierces = 1;

    [Header("References")]
    public static PlayerController instance;

    private bool _awaitingUpgrade = false;
    private Rigidbody2D rb;
    private Animator anim;
    public Sword sword;
    public bool dashing;
    public float dashCooldown = 4f;
    public float dashTimer = 0.3f;
    public float dashRate = 4f;
    Collider2D col;
    public GameObject afterImageLeft;
    public GameObject afterImageRight;
    public Transform playerTransform;




    void Awake()
    {
        // singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        GameManager.instance.InitializeScore();

        Time.timeScale = 1f; // Ensure time scale is normal at start

        GameManager.instance.gameStage = 1;
        GameManager.instance.musicStage = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Loot"))
        {
            Destroy(collision.gameObject);

            if (_awaitingUpgrade) return;

            points += collision.GetComponent<Loot>().value;
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
        if (maxPoints < 30) {
            maxPoints = Mathf.RoundToInt(maxPoints * 1.3f);
        }
        else if (maxPoints < 100) {
            maxPoints += 10;
        }
        else {
            maxPoints += 20; 
        }

        // Lower enemy spawn rates for all active spawn managers if they exist
        foreach (var spawner in FindObjectsOfType<SpawnManagerWerewolf>())
            spawner.spawnRate *= 0.8f;

        foreach (var spawner in FindObjectsOfType<SpawnManagerGolemite>())
            spawner.spawnRate *= 0.8f;

        foreach (var spawner in FindObjectsOfType<SpawnManagerGhost>())
            spawner.spawnRate *= 0.8f;

        // 2) pick 3 random upgrades
        List<UpgradeEffects.UpgradeType> picks = UpgradeEffects.Instance.ChooseUpgrades();
        string[] optionNames = picks.Select(u => u.ToString()).ToArray();
        int[] optionIndex = picks.Select(u => (int)u).ToArray();

        // 3) show UI
        UIManager.instance.ShowUpgradeScreen(optionNames, optionIndex, choice =>
        {
            // 4) apply the picked upgrade
            UpgradeEffects.UpgradeType chosen = picks[choice];
            UpgradeEffects.Instance.ApplyUpgrade(chosen);
            _awaitingUpgrade = false;
        });
    }

    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 1f;
            Debug.Log("Time scale reset to 1.");
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;

        if (!dashing)
        {

            rb.velocity = dir * speed;
            anim.SetFloat("speed", rb.velocity.magnitude);

            if (h > 0) anim.SetBool("MovingRight", true);
            else if (h < 0) anim.SetBool("MovingRight", false);

        }
        else
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                dashing = false;
            }
        }

        if (dashCooldown > 0f && !dashing)
        {
            dashCooldown -= Time.deltaTime; // Decrease the timer
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("IgnoreObstacle"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Obstacle"), false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (dashCooldown < 0f && !dashing)
            {
                rb.velocity = dir * speed * 3;
                dashing = true;
                dashTimer = 0.3f;
                dashCooldown = 4f;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("IgnoreObstacle"), true);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Obstacle"), true);
                if (h > 0)
                {
                    Vector3 spawnPosition = transform.position;
                    Instantiate(afterImageRight, spawnPosition, Quaternion.identity);
                    await Task.Delay(300);
                    Instantiate(afterImageRight, spawnPosition, Quaternion.identity);
                    await Task.Delay(300);
                    Instantiate(afterImageRight, spawnPosition, Quaternion.identity);
                }
                else if (h < 0)
                {
                    Vector3 spawnPosition = transform.position;
                    Instantiate(afterImageLeft, spawnPosition, Quaternion.identity);
                    await Task.Delay(300);
                    Instantiate(afterImageLeft, spawnPosition, Quaternion.identity);
                    await Task.Delay(300);
                    Instantiate(afterImageLeft, spawnPosition, Quaternion.identity);
                }

            }
        }
    }
}
