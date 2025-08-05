using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // Import the UI namespace to work with UI elements
using UnityEngine.SceneManagement; // Import the SceneManagement namespace to load scenes
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image hpBar;
    public TMP_Text levelText, pointsText, healthText;
    Health playerHealth;
    public GameObject loseScreen;
    public Button mainMenuButton, restartButton; // Reference to the buttons for main menu and restart
                                                 // Start is called before the first frame update
    public TMP_Text highScoreText, scoreText;
    void Start()
    {
        playerHealth = PlayerController.instance.GetComponent<Health>(); // Get the player's health component
        loseScreen.SetActive(false); // Initially hide the lose screen
        mainMenuButton.onClick.AddListener(MainMenu); // Add listener to the main menu button
        restartButton.onClick.AddListener(Restart); // Add listener to the restart button
    }

    // Update is called once per frame
    void Update()
    {
       if (scoreText != null) 
        scoreText.text = "Score: " + GameManager.instance.score;
    if (highScoreText != null)
        highScoreText.text = "High Score: " + GameManager.instance.highScore; if (PlayerController.instance == null)
        {
            hpBar.fillAmount = 0f; // If the player doesn't exist, set the health bar to empty
            return; // If the player doesn't exist, do nothing
        }

        hpBar.fillAmount = playerHealth.curHp / playerHealth.maxHp; // Update the health bar fill amount based on current and maximum health
        levelText.text = "Level: " + PlayerController.instance.level; // Update the level text with the player's current level
        pointsText.text = "Points: " + PlayerController.instance.points + "/" + PlayerController.instance.maxPoints; // Update the points text with the player's current points
        healthText.text = playerHealth.curHp + "/" + playerHealth.maxHp; // Update the health text with current and maximum health
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene to restart the game
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}
