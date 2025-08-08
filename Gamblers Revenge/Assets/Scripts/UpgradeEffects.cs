using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffects : MonoBehaviour
{
    private static UpgradeEffects _instance;

    public static UpgradeEffects Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UpgradeEffects>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("UpgradeEffects");
                    _instance = obj.AddComponent<UpgradeEffects>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    public enum UpgradeType
    {
        FireRate,
        Damage,
        Speed,
        Pierce,
        MaxHealth,
        DashCooldown
    }

    public void IncreaseFireRate()
    {
        
        PlayerController.instance.curWeapon.fireRate /= 1.15f;
        
    }

    public void IncreaseDamage()
    {
        PlayerController.instance.damage *= 1.15f;

    }

    public void IncreaseSpeed()
    {
        PlayerController.instance.speed += 1.2f;
    }

    public void IncreasePierce()
    {
        // assuming maxPierces is an instance field
        PlayerController.instance.maxPierces += 1;
    }

    public void IncreaseMaxHealth()
    {
        // Get the player's Health component and increase both max and current HP
        var player = PlayerController.instance;
        if (player == null)
            return;

        var health = player.GetComponent<Health>();
        if (health == null)
            return;

        // Increase the player's maximum health and heal to full
        health.maxHp += 5f;
        health.curHp = health.maxHp;

    }
    public void IncreaseDashCooldown()
    {
        // Assuming PlayerController has a dashCooldown field
        PlayerController.instance.dashCooldown *= 0.75f; // Decrease cooldown by 20%
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
            case UpgradeType.Pierce:
                IncreasePierce();
                break;
            case UpgradeType.MaxHealth:
                IncreaseMaxHealth();
                break;
            case UpgradeType.DashCooldown:
                IncreaseDashCooldown();
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
            UpgradeType.MaxHealth, UpgradeType.MaxHealth, UpgradeType.MaxHealth,
            UpgradeType.Pierce, UpgradeType.DashCooldown
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
