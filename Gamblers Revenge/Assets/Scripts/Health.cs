using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp = 10f; // Maximum health of the object
    public float curHp = 0f;
    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp; // Initialize current health to maximum health
    }

    // Update is called once per frame
    public void TakeDamage(float amt)
    {
        curHp -= amt; // Decrease current health by the amount of damage taken
        if (curHp <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
