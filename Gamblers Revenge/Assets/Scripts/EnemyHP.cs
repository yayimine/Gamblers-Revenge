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
    private Camera mainCam;

    void Awake()
    {
        health = GetComponent<Health>();
        if (hpSlider == null)
        {
            hpSlider = GetComponentInChildren<Slider>(true);
        }
        mainCam = Camera.main;
    }

    void Start()
    {
        if (hpSlider != null && health != null)
        {
            hpSlider.maxValue = health.maxHp;
            hpSlider.value = health.curHp;
            hpSlider.gameObject.SetActive(true);
        }
    }

    void LateUpdate()
    {
        if (hpSlider != null && health != null)
        {
            hpSlider.value = health.curHp;
            if (mainCam != null)
            {
                hpSlider.transform.rotation = mainCam.transform.rotation;
            }
        }
    }
}

