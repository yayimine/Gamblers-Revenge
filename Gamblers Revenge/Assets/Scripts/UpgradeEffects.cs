using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffects : MonoBehaviour
{

    //singleton instance
    public static UpgradeEffects instance;
    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public enum UpgradeType
    {
        FireRate,
        Damage,
        Speed,
        ProjectileSpeed,
        Pierce,
        MaxHealth
    }

    public void IncreaseFireRate()
    {
        PlayerController.instance.curWeapon.fireRate /= 1.2f;
    }

    public void IncreaseProjectileSpeed()
    {
        PlayerController.instance.shotSpeed *= 1.2f;
    }

    public void IncreaseDamage()
    {
        PlayerController.instance.damage *= 1.2f;
    }

    public void IncreaseSpeed()
    {
        PlayerController.instance.speed *= 1.2f;
    }

    public void IncreasePierce()
    {
        // assuming maxPierces is an instance field
        PlayerController.instance.maxPierces += 1;
    }

    public void IncreaseMaxHealth()
    {
        Health.instance.maxHp += 1;
        Health.instance.curHp += 1;
    }

    public void ApplyUpgrade(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.FireRate:
                IncreaseFireRate();
                break;
            case UpgradeType.Damage:
                IncreaseDamage();
                break;
            case UpgradeType.Speed:
                IncreaseSpeed();
                break;
            case UpgradeType.ProjectileSpeed:
                IncreaseProjectileSpeed();
                break;
            case UpgradeType.Pierce:
                IncreasePierce();
                break;
            case UpgradeType.MaxHealth:
                IncreaseMaxHealth();
                break;
            default:
                Debug.LogWarning("Unhandled upgrade type: " + upgradeType);
                break;
        }
    }

    public List<UpgradeType> ChooseUpgrades()
    {
        var availableUpgrades = new List<UpgradeType>
        {
            UpgradeType.FireRate, UpgradeType.FireRate, UpgradeType.FireRate,
            UpgradeType.FireRate, UpgradeType.FireRate, UpgradeType.FireRate,
            UpgradeType.Damage,   UpgradeType.Damage,   UpgradeType.Damage,
            UpgradeType.Damage,   UpgradeType.Damage,   UpgradeType.Damage,
            UpgradeType.Speed,    UpgradeType.Speed,    UpgradeType.Speed,
            UpgradeType.ProjectileSpeed, UpgradeType.ProjectileSpeed, UpgradeType.ProjectileSpeed,
            UpgradeType.MaxHealth, UpgradeType.MaxHealth, UpgradeType.MaxHealth,
            UpgradeType.Pierce
        };

        var chosenUpgrades = new List<UpgradeType>();
        while (chosenUpgrades.Count < 3)
        {
            int index = Random.Range(0, availableUpgrades.Count);
            var pick = availableUpgrades[index];
            if (!chosenUpgrades.Contains(pick))
                chosenUpgrades.Add(pick);
        }
        return chosenUpgrades;
    }


}
