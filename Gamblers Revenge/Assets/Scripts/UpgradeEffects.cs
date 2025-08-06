using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class UpgradeEffects : MonoBehaviour
{

    // Your existing methods:
    public void IncreaseFireRate(Weapon weapon)
    {
        weapon.fireRate /= 1.2f;
    }

    public void IncreaseDamage(Weapon weapon)
    {
        weapon.damage *= 1.2f;
    }

    public void IncreaseSpeed(PlayerController player)
    {
        player.speed *= 1.2f;
    }

    public void IncreasePierce(Projectile projectile)
    {
        projectile.maxPierces += 1;
    }

    public void IncreaseMaxHealth(PlayerController player)
    {
        Health playerHp = GetComponent<Health>();

        playerHp.maxHp += 1f;
        playerHp.curHp = playerHp.maxHp;

    }

    /*public void IncreaseProjectileSize(Projectile projectile)
    {
        float multiplier = 1.5f;

        SwordSlices slice = GetComponent<SwordSlices>();
        Sword slash = GetComponent<Sword>();
        Vector2 originalSize = transform.localScale;
        transform.localScale = originalSize * multiplier;
        projectile.size *= 1.2f;
    } */

    public void IncreaseProjectileSpeed(Projectile projectile)
    {
        SwordSlices slice = GetComponent<SwordSlices>();
        Sword slash = GetComponent<Sword>();


        slice.shotSpeed *= 1.2f;
        slash.slashSpeed *= 1.2f;
    }

    // New dispatcher method:
    public void ApplyUpgrade(
        UpgradeType type,
        Weapon weapon = null,
        PlayerController player = null,
        Projectile projectile = null)
    {
        switch (type)
        {
            case UpgradeType.FireRate:
                if (weapon != null) IncreaseFireRate(weapon);
                break;
            case UpgradeType.Damage:
                if (weapon != null) IncreaseDamage(weapon);
                break;
            case UpgradeType.Speed:
                if (player != null) IncreaseSpeed(player);
                break;
            case UpgradeType.Pierce:
                if (projectile != null) IncreasePierce(projectile);
                break;
            case UpgradeType.MaxHealth:
                if (player != null) IncreaseMaxHealth(player);
                break;
            /*case UpgradeType.ProjectileSize:
                if (projectile != null) IncreaseProjectileSize(projectile);
                break; */
            case UpgradeType.ProjectileSpeed:
                if (projectile != null) IncreaseProjectileSpeed(projectile);
                break;
            default:
                Debug.LogWarning($"Unhandled upgrade type: {type}");
                break;
        }
    }
}
