using UnityEngine;

public class PathInterferenceSpawner : MonoBehaviour
{
    [Tooltip("Assign your interference prefab here")]
    public GameObject interferenceZone;

    [Tooltip("0 = none, 1 = always")]
    [Range(0f, 1f)]
    public float spawnChance = 0.4f;

    [Tooltip("Delay to allow other path setups first")]
    public float spawnDelay = 0.1f;

    void Start()
    {
        Invoke("SpawnInterferenceZones", spawnDelay);
    }

    void SpawnInterferenceZones()
    {
        GameObject[] allPaths = GameObject.FindGameObjectsWithTag("Path");

        if (allPaths.Length == 0)
        {
            Debug.LogWarning("PathInterferenceSpawner: No objects tagged 'Path' found!");
            return;
        }

        Debug.Log($"Found {allPaths.Length} paths to potentially spawn interference on");

        foreach (var path in allPaths)
        {
            if (Random.value <= spawnChance)
            {
                SpawnOnPath(path);
                Debug.Log($"Spawned interference on path {path.name}");
            }
        }
    }

    void SpawnOnPath(GameObject path)
    {
        Vector3 spawnPos = path.transform.position;
        Instantiate(interferenceZone, spawnPos, Quaternion.identity, path.transform);
    }
}
