using UnityEngine;

public class Interceptor : MonoBehaviour
{
    public float health = 100f;
    public float damagePerHit = 50f;

    void OnCollisionEnter(Collision collision)
    {
        health -= damagePerHit;
        if (health <= 0f)
            Destroy(gameObject);
    }
}
