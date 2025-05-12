using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSceneLoader : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            levelButtons[i].onClick.AddListener(() => SceneManager.LoadScene("Level_" + levelIndex));
        }
    }
}
