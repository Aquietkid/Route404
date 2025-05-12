using UnityEngine;
using System.Collections.Generic;

public class ConnectionSpawner : MonoBehaviour
{
    public GameObject connectionPrefab; // Assign the long cylinder prefab in the Inspector
    public List<TransformPair> connections; // Define object pairs in the Inspector

    private Dictionary<Transform, List<Transform>> routerMap = new();

    [System.Serializable]
    public struct TransformPair
    {
        public Transform start;
        public Transform end;
    }

    void Start()
    {
        foreach (var pair in connections)
        {
            SpawnConnection(pair.start, pair.end);
        }

        // Set the connection map for the packet
        PacketController.Instance.SetRouterConnections(routerMap);
    }

    void SpawnConnection(Transform start, Transform end)
    {
        Vector3 midpoint = (start.position + end.position) / 2f;
        Vector3 direction = end.position - start.position;
        float distance = direction.magnitude;

        GameObject connection = Instantiate(connectionPrefab, midpoint, Quaternion.identity);
        connection.tag = "Path";                
        connection.transform.up = direction.normalized;
        connection.transform.localScale = new Vector3(
            connection.transform.localScale.x,
            distance / 2f,
            connection.transform.localScale.z
        );

        var collider = connection.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(0.2f, 1f, 0.2f); // Adjust if needed
        collider.center = Vector3.zero;

        var chooser = connection.AddComponent<ConnectionChooser>();
        chooser.SetDestination(end);

        // Track router connections
        if (!routerMap.ContainsKey(start)) routerMap[start] = new List<Transform>();
        routerMap[start].Add(end);
    }

}
