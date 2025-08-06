using System;
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
    public Button[] upgradeButtons;           // size = 3
    [Tooltip("The Text component on each of those buttons")]
    public TMP_Text[] upgradeButtonTexts;     // size = 3

    // A callback that your game logic can set to handle the chosen upgrade (0‒2)
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

        // Wire up each upgrade button to its index
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int idx = i;  // capture loop variable
            upgradeButtons[i].onClick.AddListener(() => OnUpgradeButton(idx));
        }
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

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    /// <summary>
    /// Call this when the player levels up.
    /// `options` must be length == upgradeButtons.Length.
    /// `onChosen` will be invoked with the index (0‒2).
    /// </summary>
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

        upgradeScreen.SetActive(true);
    }

    private void OnUpgradeButton(int idx)
    {
        // hide UI
        upgradeScreen.SetActive(false);

        // invoke the callback
        onUpgradeChosen?.Invoke(idx);
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
