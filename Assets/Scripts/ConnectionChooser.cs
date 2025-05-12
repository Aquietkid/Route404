using UnityEngine;

public class ConnectionChooser : MonoBehaviour
{
    [SerializeField] private Transform destinationRouter;

    public Transform GetDestination() => destinationRouter;
    public void SetDestination(Transform dest) => destinationRouter = dest;

    public void OnClicked()
    {
        if (destinationRouter != null)
        {
            Debug.Log($"Connection clicked. Routing to: {destinationRouter.name}");
            PacketController.Instance.SetNextDestination(destinationRouter);
        }
    }
}
