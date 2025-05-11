// InterceptorChase.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InterceptorChase : MonoBehaviour
{
    [Tooltip("Speed at which interceptor runs toward the player")]
    public float moveSpeed = 5f;

    [Tooltip("How quickly interceptor rotates to face the player")]
    public float turnSpeed = 720f; // degrees per second

    private Transform player;
    private Rigidbody rb;

    void Awake()
    {
        // Grab components
        rb = GetComponent<Rigidbody>();

        // Find the player in scene
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
            player = playerGO.transform;
        else
            Debug.LogWarning("InterceptorChase: no GameObject tagged 'Player' found!");
    }

    void Start()
    {
        if (player != null)
        {
            // Immediately face the player
            Vector3 toPlayer = player.position - transform.position;
            if (toPlayer.sqrMagnitude > 0.001f)
            {
                Quaternion lookRot = Quaternion.LookRotation(toPlayer.normalized, Vector3.up);
                transform.rotation = lookRot;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // 1) Rotate smoothly to face the player each physics frame
        Vector3 toPlayer = player.position - transform.position;
        if (toPlayer.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(toPlayer.normalized, Vector3.up);
            Quaternion newRot = Quaternion.RotateTowards(
                rb.rotation,
                targetRot,
                turnSpeed * Time.fixedDeltaTime
            );
            rb.MoveRotation(newRot);
        }

        // 2) Move forward in the direction we're facing
        Vector3 forwardMove = transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Example: take damage only from projectiles or the player
        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Projectile"))
        {
            // Implement health reduction/destroy logic here or via another script
            // e.g. GetComponent<InterceptorHealth>().TakeDamage(…);
        }
    }
}
