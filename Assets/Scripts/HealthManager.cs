using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Make sure to include this if you're using TextMeshPro

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private string gameOverPanelName = "GameOverpanel"; // Name of the panel to initially hide
    private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreText; // Reference to your Score Text UI element (TextMeshPro)
    // If you are using the regular Unity UI.Text, use:
    // [SerializeField] private UnityEngine.UI.Text scoreText;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    void Awake()
    {
        // Singleton setup
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;

        // Find the GameOverpanel within the gameOverUI and set it inactive
        if (gameOverUI != null)
        {
            Transform panelTransform = gameOverUI.transform.Find(gameOverPanelName);
            if (panelTransform != null)
            {
                gameOverPanel = panelTransform.gameObject;
                gameOverPanel.SetActive(false);
            }
            else
            {
                Debug.LogError($"Could not find a child named '{gameOverPanelName}' within the GameOverUI!");
            }
        }
        else
        {
            Debug.LogError("GameOverUI GameObject is not assigned in the Inspector!");
        }

        // Ensure the score text is initially hidden
        if (scoreText != null && gameOverUI != null)
        {
            scoreText.gameObject.SetActive(false); // Assuming scoreText is a direct child or within GameOverpanel
        }
    }

    /// <summary>
    /// Instantly kills the player.
    /// </summary>
    public void ApplyInstantDeath()
    {
        currentHealth = 0f;
        HandleDeath();
    }

    /// <summary>
    /// Applies continuous damage (per second).
    /// </summary>
    public void ApplyDamage(float dps)
    {
        if (currentHealth <= 0f) return;

        currentHealth -= dps * Time.deltaTime;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (currentHealth <= 0f)
            HandleDeath();
    }

    private void HandleDeath()
    {
        // Show Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Fallback to activating the entire canvas if the panel isn't found
        }

        // Calculate and display the final score
        if (scoreText != null)
        {
            float healthPercentage = (currentHealth / maxHealth) * 100f;
            scoreText.text = $"Final Health: {Mathf.RoundToInt(healthPercentage)}%";
            scoreText.gameObject.SetActive(true); // Make the score text visible
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Call from your Game Over “Replay” button.
    /// </summary>
    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Call from your Game Over “Home” button.
    /// </summary>
    public void GoToHomeScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScreen");
    }
}