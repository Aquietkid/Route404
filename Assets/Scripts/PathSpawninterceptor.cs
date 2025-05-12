using UnityEngine;

public class PathInterceptorSpawner : MonoBehaviour
{
    [Tooltip("Assign your interceptor prefab here")]
    public GameObject interceptor;

    [Tooltip("0 = none, 1 = always (since Random.value is [0,1])]")]
    [Range(0f, 1f)]
    public float spawnChance = 0.3f;

    [Tooltip("Small delay to ensure paths are created first")]
    public float spawnDelay = 0.1f;

    void Start()
    {
        // Delay the spawn to ensure paths are created first
        Invoke("SpawnInterceptors", spawnDelay);
    }

    void SpawnInterceptors()
    {
        // Will now return all cylinders tagged "Path"
        GameObject[] allPaths = GameObject.FindGameObjectsWithTag("Path");

        if (allPaths.Length == 0)
        {
            Debug.LogWarning("PathInterceptorSpawner: No objects tagged 'Path' found!");
            return;
        }

        Debug.Log($"Found {allPaths.Length} paths to potentially spawn interceptors on");

        foreach (var path in allPaths)
        {
            if (Random.value <= spawnChance)
            {
                SpawnOnPath(path);
                Debug.Log($"Spawned interceptor on path {path.name}");
            }
        }
    }

    void SpawnOnPath(GameObject path)
    {
        Vector3 spawnPos = path.transform.position;
        // Quaternion.identity is fine if your interceptor reorients itself
        Instantiate(interceptor, spawnPos, Quaternion.identity, path.transform);
    }
}