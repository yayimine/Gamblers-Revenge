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

    // A callback that your game logic can set to handle the chosen upgrade (0‒2)
    

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
    }

    void Update()
    {
        // Update score & high score
        /*if (scoreText != null)
            scoreText.text = "Score: " + GameManager.instance.score;
        if (highScoreText != null)
            highScoreText.text = "High Score: " + GameManager.instance.highScore;*/

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
        if (levelText != null)
            levelText.text = "Level: " + player.level;
        if (pointsText != null)
            pointsText.text = "Points: " + player.points + "/" + player.maxPoints;
        if (healthText != null)
            healthText.text = $"{playerHealth.curHp}/{playerHealth.maxHp}";
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
