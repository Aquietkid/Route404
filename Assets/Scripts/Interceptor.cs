using UnityEngine;

public class Interceptor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager.Instance.ApplyInstantDeath();
            Debug.Log("Interceptor triggered by Player!");
        }
    }

}