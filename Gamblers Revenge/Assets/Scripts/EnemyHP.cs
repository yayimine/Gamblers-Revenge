using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple helper that keeps an enemy health slider in sync with its
/// <see cref="Health"/> component.  Attach this to an enemy alongside a
/// <c>Health</c> component and assign a <see cref="Slider"/> in the inspector.
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
            hpSlider.maxValue = health.maxHp;
            hpSlider.value = health.curHp;
        }
    }

    void Update()
    {
        if (hpSlider != null && health != null)
        {
            hpSlider.value = health.curHp;
        }
    }
}

