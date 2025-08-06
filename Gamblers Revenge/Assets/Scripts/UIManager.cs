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
    public TMP_Text scoreText, highScoreText;

    Health playerHealth;
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

        if (playerHealth == null)
            playerHealth = PlayerController.instance.GetComponent<Health>();

        if (playerHealth == null)
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
    

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
