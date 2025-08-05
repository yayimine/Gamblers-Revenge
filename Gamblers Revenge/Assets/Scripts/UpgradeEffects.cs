using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum UpgradeType
{
    FireRate,
    Damage,
    Speed,
    Pierce,
    MaxHealth,
    ProjectileSize,
    ProjectileSpeed
}

[System.Serializable]
public class UpgradeOption
{
    public UpgradeType type;
    [Tooltip("Higher weight → more common")]
    public float weight = 1f;
}

public class UpgradeManager : MonoBehaviour
{
    [Header("Reference to your effects script")]
    public UpgradeEffects upgradeEffects;

    [Header("Configure rarity of each upgrade here")]
    public List<UpgradeOption> upgradeOptions;

    void Awake()
    {// Example initialization (e.g. in Awake or via default field initializer)
        upgradeOptions = new List<UpgradeOption>
        {
            new UpgradeOption { type = UpgradeType.FireRate, weight = 5f },  // Common
            new UpgradeOption { type = UpgradeType.Damage, weight = 5f },  //C
            new UpgradeOption { type = UpgradeType.Speed, weight = 5f },  //C
            new UpgradeOption { type = UpgradeType.MaxHealth, weight = 3f },  //Rare
            new UpgradeOption { type = UpgradeType.ProjectileSpeed, weight = 3f },  //R
            new UpgradeOption { type = UpgradeType.ProjectileSize, weight = 1f },  //Legendary
            new UpgradeOption { type = UpgradeType.Pierce, weight = 1f } // L
        };
    }

    
    public List<UpgradeType> GetRandomUpgrades(int count)
    {
        var result = new List<UpgradeType>();
        // Make a mutable copy of options
        var pool = new List<UpgradeOption>(upgradeOptions);

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            float totalWeight = pool.Sum(o => o.weight);
            float r = Random.Range(0f, totalWeight);
            float cum = 0f;

            foreach (var opt in pool)
            {
                cum += opt.weight;
                if (r <= cum)
                {
                    result.Add(opt.type);
                    pool.Remove(opt);
                    break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Applies each selected upgrade to the appropriate target.
    /// Supply whichever of weapon, player, projectile are relevant.
    /// </summary>
    public void ApplyUpgrades(
        List<UpgradeType> upgrades,
        Weapon weapon         = null,
        PlayerController player    = null,
        Projectile projectile = null)
    {
        foreach (var u in upgrades)
        {
            switch (u)
            {
                case UpgradeType.FireRate:
                    if (weapon != null) upgradeEffects.IncreaseFireRate(weapon);
                    break;
                case UpgradeType.Damage:
                    if (weapon != null) upgradeEffects.IncreaseDamage(weapon);
                    break;
                case UpgradeType.Speed:
                    if (player != null) upgradeEffects.IncreaseSpeed(player);
                    break;
                case UpgradeType.Pierce:
                    if (projectile != null) upgradeEffects.IncreasePierce(projectile);
                    break;
                case UpgradeType.MaxHealth:
                    if (player != null) upgradeEffects.IncreaseMaxHealth(player);
                    break;
                case UpgradeType.ProjectileSize:
                    if (projectile != null) upgradeEffects.IncreaseProjectileSize(projectile);
                    break;
                case UpgradeType.ProjectileSpeed:
                    if (projectile != null) upgradeEffects.IncreaseProjectileSpeed(projectile);
                    break;
            }
        }
    }

    // Example usage:
    // void Start() {
    //   var picks = GetRandomUpgrades(3);
    //   ApplyUpgrades(picks, myWeapon, myPlayer, myProjectile);
    // }
}
