using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Handles displaying the upgrade selection screen when the player has lost
/// and needs to choose from a set of upgrades. It configures the UI elements
/// and invokes a callback when an option is selected.
/// </summary>
public class LoseManager : MonoBehaviour
{
    public static LoseManager instance;

    [Header("Upgrade Screen")]
    public GameObject upgradeScreen;
    [Tooltip("Assign exactly 3 buttons here")]
    public Button[] upgradeButtons;           // size = 3
    [Tooltip("The Text component on each of those buttons")]
    public TMP_Text[] upgradeButtonTexts;     // size = 3

    [Header("Event on Upgrade Selected")]
    [Tooltip("Assign callbacks to handle the chosen upgrade index")]
    public UnityEvent<int> onUpgradeChosen;

    void Awake()
    {
        // singleton setup
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Ensure UI is hidden
        upgradeScreen.SetActive(false);

        // Wire up each button to invoke the event with its index
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int idx = i;  // capture loop variable
            upgradeButtons[i].onClick.AddListener(() => HandleUpgrade(idx));
        }
    }

    /// <summary>
    /// Shows the upgrade UI with given option labels (must match button count).
    /// </summary>
    public void ShowUpgradeScreen(string[] options)
    {
        if (options.Length != upgradeButtons.Length)
        {
            Debug.LogError($"You must supply exactly {upgradeButtons.Length} options!");
            return;
        }

        // Populate button texts
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtonTexts[i].text = options[i];
        }

        // Pause game and show UI
        Time.timeScale = 0f;
        upgradeScreen.SetActive(true);
    }

    /// <summary>
    /// Internal handler for button click
    /// </summary>
    private void HandleUpgrade(int idx)
    {
        // Hide UI and resume game
        upgradeScreen.SetActive(false);
        Time.timeScale = 1f;

        // Fire the event for any listeners
        onUpgradeChosen.Invoke(idx);
    }
}
