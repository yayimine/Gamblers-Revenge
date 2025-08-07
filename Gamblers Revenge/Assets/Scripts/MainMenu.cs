using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Very small controller for the main menu scene.  It simply wires the Start
/// button to load the gameplay scene when clicked.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>Reference to the Start button in the UI.</summary>
    public Button startButton;

    /// <summary>
    /// Loads the main gameplay scene. This is assigned as a callback to the
    /// Start button.
    /// </summary>
    public void StartButtonPressed()
    {
        SceneManager.LoadScene("YANGSCENE"); // Load the game scene when the button is pressed
    }

    /// <summary>
    /// Hook up the button's onClick event at startup.
    /// </summary>
    void Start()
    {
        startButton.onClick.AddListener(StartButtonPressed); // Add listener to the Start button
    }
}
