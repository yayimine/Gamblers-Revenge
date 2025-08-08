using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Threading.Tasks;
using UnityEngine;

public class Health : MonoBehaviour
{
    PlayerController player;
    //singleton
    public static Health instance; // Singleton instance of the Health class
    private Animator anim;

    public static Health GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Health>();
            if (instance == null)
            {
                GameObject obj = new GameObject("Health");
                instance = obj.AddComponent<Health>();
            }
        }
        return instance;
    }

    public float maxHp = 20f; // Maximum health of the object
    public float curHp = 0f;
    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp; // Initialize current health to maximum health

        if (gameObject.CompareTag("Player"))
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    public async void TakeDamage(float amt)
    {
        curHp -= amt; // Decrease current health by the amount of damage taken
        if (curHp <= 0f)
        {
            if (gameObject.CompareTag("Player"))
            {
                anim.SetBool("Alive", false);
                PlayerController.instance.speed= 0f;
                await Task.Delay(2000);
                UIManager.instance?.ShowLoseScreen();
                if(UIManager.instance != null)
                {
                    UIManager.instance.UpdateHealth(0, maxHp);
                }
                if(gameObject != null) Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
