using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of the GameManager

    public float highScore = 0, score = 0; // Variable to store the high score
                                           // Start is called before the first frame update


    public float gameStage = 1;
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
    }


    // Update is called once per frame
    void Update()
    {
        
        gameStage = 1 + score / 50;

    }
}
