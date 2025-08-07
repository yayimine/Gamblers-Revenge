using UnityEngine;

/// <summary>
/// Simple helper that keeps an enemy health slider in sync with its
/// <see cref="Health"/> component.  Attach this to an enemy alongside a
/// <c>Health</c> component and assign a <c>HPSlider</c> in the inspector.
/// </summary>
public class EnemyHP : MonoBehaviour
{
    public Slider hpSlider;
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        if (hpSlider != null && health != null)
        {
            hpSlider.SetMaxHealth(health.maxHp);
        }
    }

    void Update()
    {
        if (hpSlider != null && health != null)
        {
            hpSlider.SetHealth(health.curHp);
        }
    }
}

