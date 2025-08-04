using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of the GameManager

    public float highScore = 0, score = 0; // Variable to store the high score
                                           // Start is called before the first frame update
    
    public void InitializeScore()
    {
        score = 0;
    }

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the GameManager persists across scenes
        } else {
            Destroy(gameObject); // If an instance already exists, destroy this one
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
