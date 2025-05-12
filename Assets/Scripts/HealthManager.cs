using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject gameOverUI;

    private float maxHealth = 100f;
    private float currentHealth;

    void Awake() => Instance = this;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameOverUI.SetActive(false);
    }

    public void ApplyInstantDeath()
    {
        currentHealth = 0;
        UpdateHealthUI();
        HandleDeath();
    }

    public void ApplyDamage(float amount)
    {
        if (currentHealth <= 0) return;
        currentHealth -= amount * Time.deltaTime;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();

        if (currentHealth <= 0)
            HandleDeath();
    }

    void UpdateHealthUI()
    {
        healthBar.value = currentHealth / maxHealth;
    }

    void HandleDeath()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHomeScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HomeScreen"); // change to your actual scene name
    }
}
