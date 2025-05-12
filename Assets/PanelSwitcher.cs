using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject targetPanel;
    public Button switchButton;

    void Start()
    {
        targetPanel.SetActive(false);
        switchButton.onClick.AddListener(SwitchPanels);
    }

    void SwitchPanels()
    {
        currentPanel.SetActive(false);
        targetPanel.SetActive(true);
    }
}
