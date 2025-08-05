using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("In-Game HUD")]
    public Image hpBar;
    public TMP_Text levelText, pointsText, healthText;
    Health playerHealth;

    [Header("Lose Screen")]
    public GameObject loseScreen;
    public Button mainMenuButton, restartButton;
    public TMP_Text highScoreText, scoreText;

    [Header("Upgrade Screen")]
    public GameObject upgradeScreen;
    [Tooltip("Assign exactly 3 buttons here")]
    public Button[] upgradeButtons;
    [Tooltip("The Text component on each of those buttons")]
    public TMP_Text[] upgradeButtonTexts;

    // A callback that your game logic can set to handle the chosen upgrade
    private Action<int> onUpgradeChosen;

    void Awake()
    {
        // singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        playerHealth = PlayerController.instance.GetComponent<Health>();

        // Lose screen setup
        loseScreen.SetActive(false);
        mainMenuButton.onClick.AddListener(MainMenu);
        restartButton.onClick.AddListener(Restart);

        // Upgrade screen setup
        upgradeScreen.SetActive(false);
        foreach (var btn in upgradeButtons)
            btn.onClick.AddListener(() => OnUpgradeButtonClicked(btn));
    }

    void Update()
    {
        // Update score & high score
        if (scoreText != null)
            scoreText.text = "Score: " + GameManager.instance.score;
        if (highScoreText != null)
            highScoreText.text = "High Score: " + GameManager.instance.highScore;

        // Update HUD
        if (PlayerController.instance == null)
        {
            hpBar.fillAmount = 0f;
            return;
        }
        hpBar.fillAmount = playerHealth.curHp / playerHealth.maxHp;
        levelText.text = "Level: " + PlayerController.instance.level;
        pointsText.text = "Points: " 
            + PlayerController.instance.points 
            + "/" + PlayerController.instance.maxPoints;
        healthText.text = $"{playerHealth.curHp}/{playerHealth.maxHp}";
    }

    // Public: call this when the player dies
    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    // Public: call this when the player levels up and you want to show 3 upgrade choices
    // `options` should be exactly 3 strings describing each upgrade,
    // `onChosen` is your callback(0‒2) to actually apply it.
    public void ShowUpgradeScreen(string[] options, Action<int> onChosen)
    {
        if (options.Length != upgradeButtons.Length)
        {
            Debug.LogError($"You must supply exactly {upgradeButtons.Length} options!");
            return;
        }

        // store callback
        onUpgradeChosen = onChosen;

        // populate button texts and enable
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtonTexts[i].text = options[i];
        }

        // show UI
        upgradeScreen.SetActive(true);
    }

    // Internal handler for any upgrade button
    private void OnUpgradeButtonClicked(Button btn)
    {
        // find index
        int idx = Array.IndexOf(upgradeButtons, btn);
        if (idx < 0) return;

        // hide upgrade UI
        upgradeScreen.SetActive(false);

        // invoke game logic
        onUpgradeChosen?.Invoke(idx);
        onUpgradeChosen = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
