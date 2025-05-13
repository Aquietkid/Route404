using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarController : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();

        // 1) Normalize the slider range [0,1]
        slider.minValue = 0f;
        slider.maxValue = 1f;

        // 2) Hide / remove the handle so it’s just a bar
        if (slider.handleRect != null)
            slider.handleRect.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // Start polling health every frame
        InvokeRepeating(nameof(UpdateBar), 0f, 0f);
    }

    void OnDisable()
    {
        CancelInvoke(nameof(UpdateBar));
    }

    private void UpdateBar()
    {
        if (HealthManager.Instance == null) return;

        // Set fill = current / max
        float t = HealthManager.Instance.CurrentHealth / HealthManager.Instance.MaxHealth;
        slider.value = Mathf.Clamp01(t);
    }
}