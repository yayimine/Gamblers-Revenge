using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton; // Reference to the Start button

    public void StartButtonPressed()
    {
        SceneManager.LoadScene("EvilCastle"); // Load the game scene when the button is pressed
    }
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartButtonPressed); // Add listener to the Start button
    }

    // Update is called once per frame
}
