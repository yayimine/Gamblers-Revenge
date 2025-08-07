using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central manager that tracks the player's score, high score and overall game
/// progression.  Exposes a singleton instance for easy access from other
/// scripts.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>Singleton instance of the GameManager.</summary>
    public static GameManager instance;

    /// <summary>Current and highest score achieved during the session.</summary>
    public float highScore = 0, score = 0;

    /// <summary>
    /// Overall game difficulty progression. Higher stages spawn enemies faster
    /// and also drive music changes.
    /// </summary>
    public float gameStage = 1;
    /// <summary>Tracks which OST is currently playing.</summary>
    public float musicStage = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /// <summary>Reset the player's score to zero.</summary>
    public void InitializeScore()
    {
        score = 0;
    }

    /// <summary>Increase the current score by a given amount and update high score.</summary>
    public void UpdateScore(float amt)
    {
        score += amt; // Update the score by the specified amount
        if (score > highScore)
        {
            highScore = score; // Update high score if the current score exceeds it
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Simple formula that scales game difficulty with score.
        gameStage = 1 + score / 50;
    }
}
