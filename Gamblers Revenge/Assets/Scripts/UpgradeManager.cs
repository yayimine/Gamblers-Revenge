using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

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
    public Rarity rarity;
}

public class UpgradeManager : MonoBehaviour
{
    [Header("Reference to your effects script")]
    public UpgradeEffects upgradeEffects;

    [Header("Define your upgrades and assign each a rarity")]
    public List<UpgradeOption> upgradeOptions;

    // Rarity weights: Common 6, Rare 3, Legendary 1
    private readonly Dictionary<Rarity, float> _rarityWeights = new Dictionary<Rarity, float>
    {
        { Rarity.Common,    6f },
        { Rarity.Rare,      3f },
        { Rarity.Legendary, 1f }
    };

    /// <summary>
    /// Picks `count` unique upgrades based on rarity weights.
    /// </summary>
    public List<UpgradeType> GetRandomUpgrades(int count)
    {
        var result = new List<UpgradeType>();

        while (result.Count < count)
        {
            // 1) Pick a rarity slot
            float totalRarityWeight = _rarityWeights.Values.Sum();
            float r = Random.Range(0f, totalRarityWeight);
            float cum = 0f;
            Rarity chosenRarity = Rarity.Common;
            foreach (var kv in _rarityWeights)
            {
                cum += kv.Value;
                if (r <= cum)
                {
                    chosenRarity = kv.Key;
                    break;
                }
            }

            // 2) From that rarity, pick a random upgrade that isn't already chosen
            var pool = upgradeOptions
                .Where(o => o.rarity == chosenRarity && !result.Contains(o.type))
                .ToList();

            if (pool.Count == 0)
            {
                // nothing left in this tier—just retry picking
                continue;
            }

            var pick = pool[Random.Range(0, pool.Count)];
            result.Add(pick.type);
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
        PlayerController playerSpeed    = null,
        Health playerHp = null,
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
                    if (playerSpeed != null) upgradeEffects.IncreaseSpeed(playerSpeed);
                    break;
                case UpgradeType.Pierce:
                    if (projectile != null) upgradeEffects.IncreasePierce(projectile);
                    break;
                case UpgradeType.MaxHealth:
                    if (playerHp != null) upgradeEffects.IncreaseMaxHealth(playerHp);
                    break;
                /*case UpgradeType.ProjectileSize:
                    if (projectile != null) upgradeEffects.IncreaseProjectileSize(projectile);
                    break;*/
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
