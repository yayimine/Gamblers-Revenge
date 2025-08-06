using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Upgrade Screen")]
    public GameObject upgradeScreen;
    [Tooltip("Assign exactly 3 buttons here")]
    public Button[] upgradeButtons;           // size = 3
    [Tooltip("The Text component on each of those buttons")]
    public TMP_Text[] upgradeButtonTexts;     // size = 3

    void Awake()
    {
        // singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Upgrade screen setup
        upgradeScreen.SetActive(false);

        // Wire up each upgrade button to its index
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int idx = i;  // capture loop variable
            upgradeButtons[i].onClick.AddListener(() => OnUpgradeButton(idx));
        }
    }

    public void ShowUpgradeScreen(string[] options, Action<int> onChosen)
    {
        if (options.Length != upgradeButtons.Length)
        {
            Debug.LogError($"You must supply exactly {upgradeButtons.Length} options!");
            return;
        }

        onUpgradeChosen = onChosen;

        // Populate button texts and ensure they’re active
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtonTexts[i].text = options[i];
        }

        Time.timeScale = 0f; // Pause the game

        upgradeScreen.SetActive(true);
    }
     private void OnUpgradeButton(int idx)
    {
        // hide UI
        upgradeScreen.SetActive(false);

        // invoke the callback
        onUpgradeChosen?.Invoke(idx);
    }
}
