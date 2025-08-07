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
    public Image xpBar;
    public TMP_Text levelText, pointsText, healthText;
    public TMP_Text scoreText, highScoreText;
    Health playerHealth;

    public List<Sprite> upgradeButtonSprites; // size = 3

    [Header("Lose Screen")]
    public GameObject loseScreen;

    [Header("Upgrade Screen")]
    public GameObject upgradeScreen;
    [Tooltip("Assign exactly 3 buttons here")]
    public Button[] upgradeButtons;           // size = 3
    [Tooltip("The Text component on each of those buttons")]
    public TMP_Text[] upgradeButtonTexts;     // size = 3

    // Callback to invoke when an upgrade is chosen
    private Action<int> onUpgradeChosen;

    void Awake()
    {
        // singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (PlayerController.instance != null)
            playerHealth = PlayerController.instance.GetComponent<Health>();
        if (loseScreen != null)
            loseScreen.SetActive(false);

        // Upgrade screen setup
        if (upgradeScreen != null)
            upgradeScreen.SetActive(false);
        if (upgradeButtons != null)
        {
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                int idx = i; // capture loop variable
                upgradeButtons[i].onClick.AddListener(() => OnUpgradeButton(idx));
            }
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
        var player = PlayerController.instance;
        if (player == null)
        {
            if (hpBar != null)
                hpBar.fillAmount = 0f;
            return;
        }

        if (playerHealth == null)
            playerHealth = player.GetComponent<Health>();

        if (playerHealth == null)
        {
            if (hpBar != null)
                hpBar.fillAmount = 0f;
            return;
        }

        if (hpBar != null)
            hpBar.fillAmount = playerHealth.curHp / playerHealth.maxHp;
        if (xpBar != null)
            xpBar.fillAmount = (float)player.points / player.maxPoints;
        if (levelText != null)
            levelText.text = "Level: " + player.level;
        if (pointsText != null)
            pointsText.text = player.points + "/" + player.maxPoints;
        if (healthText != null)
            healthText.text = $"{playerHealth.curHp}/{playerHealth.maxHp}";
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLoseScreen()
    {
        if (loseScreen != null)
            loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowUpgradeScreen(string[] options, int[] optionIndexes,  Action<int> onChosen)
    {
        if (options.Length != upgradeButtons.Length)
        {
            Debug.LogError($"You must supply exactly {upgradeButtons.Length} options!");
            return;
        }

        onUpgradeChosen = onChosen;

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].GetComponent<Image>().sprite = upgradeButtonSprites[optionIndexes[i]];
            upgradeButtonTexts[i].text = options[i];
        }

        Time.timeScale = 0f; // Pause the game
        if (upgradeScreen != null)
            upgradeScreen.SetActive(true);
    }

    private void OnUpgradeButton(int idx)
    {
        if (upgradeScreen != null)
            upgradeScreen.SetActive(false);

        Time.timeScale = 1f; // resume game

        onUpgradeChosen?.Invoke(idx);
        onUpgradeChosen = null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void HideOnSceneLoad()
    {
        var screen = GameObject.Find("UpgradeScreen");
        if (screen != null)
        {
            screen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
