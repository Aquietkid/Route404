using UnityEngine;
using System.Collections;


public class InterferenceZone : MonoBehaviour
{
   [SerializeField] private float damagePerSecond = 10f;
    private bool damaging = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            damaging = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            damaging = false;
    }

    void Update()
    {
        // if (damaging)
        //     HealthManager.Instance.ApplyDamage(damagePerSecond);
    }
}
