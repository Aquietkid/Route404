using UnityEngine;
using System.Collections.Generic;

public class Router : MonoBehaviour
{
    public List<Transform> connectedRouters;
    public Transform selectedPath; // Set this if a specific path is chosen

    public void RoutePacket(GameObject packet)
    {
        Transform nextTarget = selectedPath != null && connectedRouters.Contains(selectedPath)
            ? selectedPath
            : connectedRouters[Random.Range(0, connectedRouters.Count)];

        MovePacket(packet, nextTarget.position);
    }

    void MovePacket(GameObject packet, Vector3 destination)
    {
        // Replace with your movement logic
        packet.transform.position = destination;
    }
}
