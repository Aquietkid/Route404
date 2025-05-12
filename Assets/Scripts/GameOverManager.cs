using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;
    public Button replayButton;
    public TMP_Text gameOverText;

    void Awake()
    {
        // Ensure hidden at start
        gameOverPanel.SetActive(false);
        replayButton.onClick.AddListener(OnReplayClicked);
    }

    public void TriggerGameOver()
    {
        // Show UI
        gameOverPanel.SetActive(true);
        // Optionally pause game:
        Time.timeScale = 0f;
    }

    private void OnReplayClicked()
    {
        // Unpause
        Time.timeScale = 1f;
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
