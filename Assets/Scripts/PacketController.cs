using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PacketController : MonoBehaviour
{
    public static PacketController Instance;

    public float speed = 10f;
    public Transform currentRouter;
    private Transform nextRouter;

    private bool isMoving = false;
    private Dictionary<Transform, List<Transform>> routerConnections = new();

    void Awake() => Instance = this;

    public void SetRouterConnections(Dictionary<Transform, List<Transform>> connections)
    {
        routerConnections = connections;
    }

    public void SetNextDestination(Transform dest)
    {
        Debug.Log($"Trying to set next destination: {dest.name}");
        if (routerConnections.ContainsKey(currentRouter) && routerConnections[currentRouter].Contains(dest))
        {
            nextRouter = dest;
        }
    }


    void Start()
    {
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        Debug.Log("Starting movement loop");

        while (true)
        {
            // Wait until destination is chosen
            while (nextRouter == null)
            {
                yield return null;
            }

            isMoving = true;

            while (Vector3.Distance(transform.position, nextRouter.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextRouter.position, speed * Time.deltaTime);
                yield return null;
            }

            transform.position = nextRouter.position;
            currentRouter = nextRouter;
            nextRouter = null;

            // Wait a frame to let SetNextDestination() run, else random fallback
            yield return new WaitForSeconds(0.1f);

            if (nextRouter == null)
            {
                var options = routerConnections[currentRouter];
                nextRouter = options[Random.Range(0, options.Count)];
            }

            isMoving = false;
        }
    }
}