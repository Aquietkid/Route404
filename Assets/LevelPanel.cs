using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelPanel : MonoBehaviour
{
    [System.Serializable]
    public class LevelData
    {
        public string sceneName;
    }

    public GameObject levelButtonPrefab;
    public Transform buttonContainer;
    public List<LevelData> levels;

    void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            var buttonObj = Instantiate(levelButtonPrefab, buttonContainer);
            var text = buttonObj.GetComponentInChildren<Text>();
            if (text != null)
                text.text = (i + 1).ToString();

            var button = buttonObj.GetComponent<Button>();
            string scene = levels[i].sceneName;
            if (button != null)
                button.onClick.AddListener(() => SceneManager.LoadScene(scene));
        }
    }
}
